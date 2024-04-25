using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    NONE = -1 ,EQUIPMENT, EGG
}
[System.Serializable]
public class Item
{
    [SerializeField] int id;
    [SerializeField] int amount;
    string name;
    string desc;
    ItemType type;
    string spritePath;

    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public string Desc { get { return desc; } }
    public ItemType Type { get { return type; } }
    public string SpritePath { get { return spritePath; } }
    public int Amount { get { return amount; } }
    public Item()
    {

    }
    public Item(int id, string name, ItemType type, string desc,string spritePath, int amount = 1)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.desc = desc;
        this.spritePath = spritePath;
        this.amount = amount;
    }

    public bool Use(int amount = 1)
    {
        if (this.amount - amount < 0)
        {
            Debug.LogWarning("아이템 부족");
            return false;
        }
        else
        {
            this.amount -= amount;
            Debug.Log("아이템 사용 완료");
            return true;
        }
    }
}
