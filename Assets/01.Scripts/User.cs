using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    [SerializeField] List<TamaStat> tamas;
    [SerializeField] List<Egg> eggs;
    [SerializeField] Inventory inventory;
    [SerializeField] string nickname;

    public List<TamaStat> Tamas { get => tamas; }
    public List<Egg> Eggs { get => eggs; }
    public Inventory Inventory { get => inventory; }
    public string Nickname { get => nickname; }

    public User()
    {
        inventory = new Inventory();
        tamas = new List<TamaStat>();
        eggs = new List<Egg>();
        nickname = "test";
    }

    public void AddTama()
    {
        TamaStat tama = new TamaStat();
        tama.InitStat();
        tamas.Add(tama);
    }
    

    public void SetUser(User user)
    {
        inventory = user.inventory;
        nickname = user.nickname;
        tamas = user.tamas;
        eggs = user.eggs;
    }
}
