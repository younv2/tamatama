using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    List<Item> itemList = new List<Item>();
    public List<Item> ItemList { get { return itemList; } }
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        LoadItemData();
        Debug.Log("DataManager Awaked");
    }
    public void LoadItemData()
    {
        itemList.Clear();
        var data = CSVReader.Read("CSV/item");
        for(int i = 0; i < data.Count; i++)
        {
            Item item = new Item((int)data[i]["Id"], data[i]["Name"].ToString(), 
                (ItemType)Enum.Parse(typeof(ItemType), data[i]["ItemType"].ToString()), data[i]["Desc"].ToString(), data[i]["SpriteName"].ToString());

            itemList.Add(item);
        }
        
    }
}
