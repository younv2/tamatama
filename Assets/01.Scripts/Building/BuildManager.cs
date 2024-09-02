/*
 * 파일명 : BuildManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/5/26
 * 최종 수정일 : 2024/5/26
 * 파일 설명 : 건축 시스템 관리자
 * 수정 내용 :
 * 2024/5/26 - 건축 시스템 틀 작성 
 * 2024/5/29~2024/6/1 - 건물을 관리하기 위한 틀 작성 
 * 2024/6/8 건축물 겹쳤을 때 승인 버튼을 눌렀을 때 생기는 버그 수정
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static Define;


public class BuildManager : MonoSingleton<BuildManager>
{
    private MapState mapState = MapState.NONE;

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Tilemap tempTilemap;

    [SerializeField] private GameObject BuildSelectUI;
    private Button acceptBtn;
    private Button cancelBtn;

    public GridLayout GridLayout { get { return gridLayout; } }
    public Tilemap MainTilemap { get { return mainTilemap; } }
    public Tilemap TempTilemap { get { return tempTilemap; } }
    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();
    // Start is called before the first frame update
    private void Start()
    {
        InitSetting();
    }
    private void Update()
    {
        if (!temp)
        {
            return;
        }
        if(Input.GetMouseButton(0))
        {
            //마우스가 UI 좌표에 있을 경우
            if(Define.IsPointerOverUIObject(Input.mousePosition))
            {
                return;
            }
            if(!temp.Placed)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                if(prevPos != cellPos)
                {
                    BuildSelectUI.transform.position = gridLayout.CellToLocalInterpolated(cellPos +
                        new Vector3(0.5f, 0.5f, 0f));
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + 
                        new Vector3(0.5f , 0.5f, 0f));
                    prevPos = cellPos;
                    FollowBuilding();
                }
            }
        }
    }
    public void InitSetting()
    {
        string tilePath = @"Tiles\";
        tileBases.Add(TileType.NONE, null);
        tileBases.Add(TileType.WHITE, Resources.Load<TileBase>(tilePath + "White"));
        tileBases.Add(TileType.GREEN, Resources.Load<TileBase>(tilePath + "Green"));
        tileBases.Add(TileType.RED, Resources.Load<TileBase>(tilePath + "Red"));

        acceptBtn = BuildSelectUI.transform.Find("AcceptBtn").GetComponent<Button>();
        cancelBtn = BuildSelectUI.transform.Find("CancelBtn").GetComponent<Button>();
        acceptBtn.onClick.AddListener(() =>
        {
            AcceptBuild();
        });
        cancelBtn.onClick.AddListener(() =>
        {
            CancelBuild();
        });
    }
    public void CancelBuild()
    {
        ClearArea();
        if(temp != null)
            Destroy(temp.gameObject);
        ShowUI(false);
    }
    public void AcceptBuild()
    {
        if (temp.CanbePlaced())
        {
            if(GameManager.Instance.user.Inventory.SpendGold(DataManager.Instance.FindBuildingShopDataWithId(temp.Id).Gold))
            {
                temp.Place();
                GameManager.Instance.user.Buildings.Add(new BuildingSaveData(temp.Id, temp.area.position));
                temp = null;
                ShowUI(false);
            }
            else
            {
                Debug.Log("돈 없어");
            }
        }
    }
    public void SetMapState(MapState state)
    {
        mapState = state;
    }
    public MapState GetMapState()
    {
        return mapState;
    }
    public void CreateBuilding(Building building)
    {
        CancelBuild();
        ShowUI(true);
        temp = Instantiate(Resources.Load<GameObject>(@"Prefabs\Object\" + building.PrefabName), new Vector3(0, 0.25f, 0), Quaternion.identity).AddComponent<Building>();
        temp.DeepCopy(building);
        FollowBuilding();
    }
    public void SetBuildingWithLoadData(int id, Vector3Int position)
    {
        Building building = DataManager.Instance.FindBuildingWithId(id);
        
        temp = Instantiate(Resources.Load<GameObject>(@"Prefabs\Object\" + DataManager.Instance.FindBuildingWithId(id).PrefabName), Vector3.zero, Quaternion.identity).AddComponent<Building>();
        temp.transform.localPosition = gridLayout.CellToLocalInterpolated(position + new Vector3(.5f, .5f, 0f));
        temp.DeepCopy(building);
        temp.area.position = position;
        temp.Place();
        temp = null;

    }
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear,TileType.NONE);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }
    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);
        foreach (var b in baseArray)
        {
            if(b!= tileBases[TileType.WHITE])
            {
                Debug.Log("Cannot place here");
                return false;
            }    
        }
        return true;
    }
    private void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i =0; i< baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.WHITE])
            {
                tileArray[i] = tileBases[TileType.GREEN];
            }
            else
            {
                FillTiles(tileArray, TileType.RED);
                break;
            }
        }
        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.NONE, tempTilemap);
        SetTilesBlock(area, TileType.GREEN, mainTilemap);
    }
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x* area.size.y * area.size.z];
        int counter = 0;

        foreach( var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }
    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }
    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i =0; i< arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }
    public void ShowUI(bool isShow)
    {
        mapState = isShow == true ?  MapState.BUILDING : MapState.NONE;
        BuildSelectUI.transform.position = new Vector3(0, 0, 0);
        BuildSelectUI.SetActive(isShow);
    }
}
