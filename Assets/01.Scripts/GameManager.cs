using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public User user;
    protected override void Awake()
    {
        base.Awake();

        user = new User();
        BackgroundUI.Instance.Init();
        SpriteManager.OnLoadAllSprite();
        Debug.Log("GameManager Awaked");
    }
    private void Start()
    {
        SoundManager soundManager = new SoundManager();
        soundManager.Init();
        
        user.Inventory.AddItem(DataManager.Instance.ItemList.Find(x => x.Id == 4));
        user.Inventory.AddItem(DataManager.Instance.ItemList.Find(x => x.Id == 1));
        user.Inventory.AddItem(DataManager.Instance.ItemList.Find(x => x.Id == 2));
        user.Inventory.AddItem(DataManager.Instance.ItemList.Find(x => x.Id == 3));
        user.Inventory.AddItem(DataManager.Instance.ItemList.Find(x => x.Id == 5));
        foreach (var temp in user.Eggs)
        {
            StartCoroutine(temp.StartHatching());
        }
        
    }
}
