/*
 * ���ϸ� : DataManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ��ü �����͵��� �Ѱ� �����ϴ� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
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
