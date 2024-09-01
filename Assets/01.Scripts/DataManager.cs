/*
 * 파일명 : DataManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 전체 데이터들을 총괄 관리하는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/6/8 - 건물 관련 csv 불러오기 추가
 */

using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    #region Properties
    public List<Item> ItemList { get ; } = new List<Item>();
    public List<Building> buildingList { get ; } = new List<Building>();
    public List<BuildingShopData> buildingShopDataList { get; } = new List<BuildingShopData>();

    public List<Dungeon> dungeonList { get; } = new List<Dungeon>();

    public List<TamaLevelStatsData> tamaLvStatsList { get; } = new List<TamaLevelStatsData>();
    #endregion

    #region Methods
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        LoadItemData();
        LoadBuildingData();
        LoadDungeonData();
        LoadTamaLevelStatData();
        Debug.Log("DataManager Awaked");
    }
    public void LoadItemData()
    {
        ItemList.Clear();
        var data = CSVReader.Read("CSV/Item");
        for(int i = 0; i < data.Count; i++)
        {
            Item item = new Item((int)data[i]["Id"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["SpriteName"].ToString());

            ItemList.Add(item);
        }
        data = CSVReader.Read("CSV/EggItem");
        for (int i = 0; i < data.Count; i++)
        {
            Item item = new EggItem((int)data[i]["Id"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["SpriteName"].ToString(), (int)data[i]["MaxAmount"]);

            ItemList.Add(item);
        }
    }
    public void LoadBuildingData()
    {
        buildingList.Clear();
        var data = CSVReader.Read("CSV/Building");
        for (int i = 0; i < data.Count; i++)
        {
            Building item = new Building((int)data[i]["Id"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["PrefabName"].ToString(), (int)data[i]["SizeX"], (int)data[i]["SizeY"]);

            buildingList.Add(item);
        }

        buildingShopDataList.Clear();
        data = CSVReader.Read("CSV/BuildingShopData");
        for (int i = 0; i < data.Count; i++)
        {
            BuildingShopData item = new BuildingShopData((int)data[i]["Id"], (int)data[i]["Gold"]);

            buildingShopDataList.Add(item);
        }

    }
    public void LoadDungeonData()
    {
        dungeonList.Clear();
        var data = CSVReader.Read("CSV/Dungeon");
        for (int i = 0; i < data.Count; i++)
        {
            Dungeon item = new Dungeon((int)data[i]["Id"], data[i]["Name"].ToString(), data[i]["Desc"].ToString());

            dungeonList.Add(item);
        }
    }
    public void LoadTamaLevelStatData()
    {
        tamaLvStatsList.Clear();
        var data = CSVReader.Read("CSV/TamaLevelStats");
        for (int i = 0; i < data.Count; i++)
        {
            TamaLevelStatsData item = new TamaLevelStatsData((int)data[i]["Id"], (int)data[i]["Level"],
                (int)data[i]["Str"], (int)data[i]["Dex"], (int)data[i]["Int"], (int)data[i]["Luc"], (int)data[i]["Con"], (int)data[i]["Res"]);

            tamaLvStatsList.Add(item);
        }
    }
    public Dungeon FindDungeonWithId(int id)
    {
        return dungeonList.Find(x => x.Id == id);
    }
    public TamaLevelStatsData FindTamaLevelStatWithId(int id,int lv)
    {
        return tamaLvStatsList.Find(x => x.Id == id && x.Lv == lv);
    }
    public Building FindBuildingWithId(int id)
    {
        return buildingList.Find(x => x.Id == id);
    }
    public Building FindBuildingWithPrefabName(string name)
    {
        return buildingList.Find(x => x.PrefabName == name);
    }
    public Item FindItemWithId(int id)
    {
       return ItemList.Find(x => x.Id == id);
    }
        
    #endregion
}
