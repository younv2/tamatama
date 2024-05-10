/*
 * 파일명 : DataManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 전체 데이터들을 총괄 관리하는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    #region Properties
    public List<Item> ItemList { get ; } = new List<Item>();
    #endregion

    #region Methods
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        LoadItemData();
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
    public Item FindItemWithId(int id)
    {
       return ItemList.Find(x => x.Id == id);
    }
        
    #endregion
}
