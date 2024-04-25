using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] List<Item> itemList = new List<Item>();

    [SerializeField] int gold;
    [SerializeField] int cash;
    public List<Item> ItemList { get { return itemList; } }
    public Inventory() 
    {
        gold = 500;
        cash = 500;
    }
    
    public int GetGold () { return gold; }
    public int GetCash () { return cash;}

    public void AddItem(Item item)
    {
        itemList.Add (item);
    }
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }
    public void RemoveItem(int index)
    {
        itemList.RemoveAt(index);
    }

    public bool UseItem(int index, int amount = 1, ItemType itemType = ItemType.NONE)
    {
        List<int> indexs = new List<int>();
        //아이템 타입을 정의했을 경우 해당 아이템 리스트의 인덱스를 이용해
        if(itemType != ItemType.NONE)
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                if(itemList[i].Type == itemType)
                    indexs.Add(i);
            }
            index = indexs[index];
        }
        
        if (itemList[index].Use(amount))
        {
            if (itemList[index].Amount <= 0)
            {
                RemoveItem(index);
                Debug.Log("사용됨, 갯수 0개로 인하여 삭제");
            }
            return true;
        }

        return false;
    }
    public bool UseItem(Item item)
    {
        List<int> indexs = new List<int>();
        Item _item = itemList.Find(x => x == item);
        if (_item != null)
        {
            if (_item.Amount <= 0)
            {
                RemoveItem(_item);
                Debug.Log("사용됨, 갯수 0개로 인하여 삭제");
            }
            return true;
        }

        return false;
    }
}
