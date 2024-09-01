/*
 * 파일명 : Building.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/5/29
 * 최종 수정일 : 2024/5/29
 * 파일 설명 : 건물 클래스
 * 수정 내용 :
 * 2024/5/26 - 건물 사이즈 및 위치 저장을 위한 변수 작성 
 * 2024/6/8 - 건물 데이터를 저장해두기 위한 변수 선언 및 생성자 생성
 */
using Newtonsoft.Json;
using UnityEngine;
[System.Serializable]
public class Building : MonoBehaviour
{
    [JsonIgnore] public BoundsInt area;
    [JsonIgnore] private new string name;
    [JsonIgnore] private string desc;
    [JsonIgnore] private string prefabName;

    private int id;

    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public string Desc { get { return desc; } }
    public string PrefabName { get { return prefabName; } }
    

    public Building(int id, string name, string desc, string prefabName, int sizeX, int sizeY)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.prefabName = prefabName;
        area.size  = new Vector3Int(sizeX, sizeY, 1);

    }

    public bool Placed { get; private set; }
    // Start is called before the first frame update
    public bool CanbePlaced()
    {
        Vector3Int positionInt = BuildManager.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(BuildManager.Instance.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Place()
    {
        Vector3Int positionInt = BuildManager.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        BuildManager.Instance.TakeArea(areaTemp);
    }
    public void DeepCopy(Building building)
    {
        id = building.id;
        name = building.name;
        desc = building.desc;
        prefabName = building.prefabName;
        area = building.area;
    }
}
