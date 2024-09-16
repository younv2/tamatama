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
    public List<Item> ItemLst { get ; } = new List<Item>();
    public List<Building> BuildingLst { get ; } = new List<Building>();
    public List<BuildingShopData> BuildingShopDataLst { get; } = new List<BuildingShopData>();
    public List<DungeonData> DungeonLst { get; set; } = new List<DungeonData>();
    public List<TamaLevelStatsData> tamaLvStatsLst { get; } = new List<TamaLevelStatsData>();
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
        ItemLst.Clear();
        var data = CSVReader.Read("CSV/Item");
        for(int i = 0; i < data.Count; i++)
        {
            Item item = new Item((int)data[i]["Id"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["SpriteName"].ToString());

            ItemLst.Add(item);
        }
        data = CSVReader.Read("CSV/EggItem");
        for (int i = 0; i < data.Count; i++)
        {
            Item item = new EggItem((int)data[i]["Id"], (int)data[i]["TribeId"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["SpriteName"].ToString(), (int)data[i]["MaxAmount"]);

            ItemLst.Add(item);
        }
    }
    public void LoadBuildingData()
    {
        BuildingLst.Clear();
        var data = CSVReader.Read("CSV/Building");
        for (int i = 0; i < data.Count; i++)
        {
            Building item = new Building((int)data[i]["BuildingId"], data[i]["Name"].ToString(), data[i]["Desc"].ToString(), data[i]["PrefabName"].ToString(), (int)data[i]["SizeX"], (int)data[i]["SizeY"]);

            BuildingLst.Add(item);
        }

        BuildingShopDataLst.Clear();
        data = CSVReader.Read("CSV/BuildingShopData");
        for (int i = 0; i < data.Count; i++)
        {
            BuildingShopData item = new BuildingShopData((int)data[i]["BuildingId"], (int)data[i]["Gold"]);

            BuildingShopDataLst.Add(item);
        }

    }
    public void LoadDungeonData()
    {
        // Resources 폴더에서 DungeonData 스크립터블 오브젝트들을 모두 로드
        DungeonData[] dungeons = Resources.LoadAll<DungeonData>("Dungeons");

        // 리스트에 던전 데이터들을 추가
        DungeonLst = new List<DungeonData>(dungeons);

        DungeonLst.Sort((a,b)=>a.dungeonId.CompareTo(b.dungeonId));
        Debug.Log($"Loaded {DungeonLst.Count} dungeons.");

    }
    public void LoadTamaLevelStatData()
    {
        tamaLvStatsLst.Clear();
        var data = CSVReader.Read("CSV/TamaLevelStats");
        for (int i = 0; i < data.Count; i++)
        {
            TamaLevelStatsData item = new TamaLevelStatsData((int)data[i]["TribeId"], (int)data[i]["Level"],
                (int)data[i]["Str"], (int)data[i]["Dex"], (int)data[i]["Int"], (int)data[i]["Luc"], (int)data[i]["Con"], (int)data[i]["Res"]);

            tamaLvStatsLst.Add(item);
        }
    }
    public DungeonData FindDungeonWithId(int id)
    {
        return DungeonLst.Find(x => x.dungeonId == id);
    }
    public TamaLevelStatsData FindTamaLevelStatWithId(int id,int lv)
    {
        return tamaLvStatsLst.Find(x => x.Id == id && x.Lv == lv);
    }
    public Building FindBuildingWithId(int id)
    {
        return BuildingLst.Find(x => x.Id == id);
    }
    public BuildingShopData FindBuildingShopDataWithId(int id)
    {
        return BuildingShopDataLst.Find(x => x.Id == id);
    }
    public Building FindBuildingWithPrefabName(string name)
    {
        return BuildingLst.Find(x => x.PrefabName == name);
    }
    public Item FindItemWithId(int id)
    {
       return ItemLst.Find(x => x.Id == id);
    }
        
    #endregion
}
