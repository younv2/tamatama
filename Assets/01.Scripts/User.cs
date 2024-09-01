/*
 * ���ϸ� : User.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/11
 * ���� ���� : ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/7 - ������Ƽ�� ������ ���� �ʴ� ���� Ȯ���Ͽ� ������ ����
 * 2024/5/11 - ������ ����� �����ʾ� Newtonsoft.Json���� ���� �� �ش� ���̺귯���� �°� ����
 * 2024/6/8 - ���๰ ������ ����ǵ��� ����
 * 2024/8/29 - Ÿ���� �������� ���� �� �ֵ��� ���� 
 * 2024/9/2 - ��ȭ ���� ������ �ε尡 ���� �ʴ� ���� ����
 */
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class User
{
    #region Variables
    private int hatchEggsLimitCount = 3;
    #endregion

    #region Properties
    public List<TamaStat> Tamas { get; set; }
    public List<Egg> Eggs { get; set; }
    public Inventory Inventory { get; set; }
    public string Nickname { get; set; }
    public List<BuildingSaveData> Buildings { get; set; }
    #endregion

    #region Constructor
    public User()
    {
        Inventory = new Inventory();
        Tamas = new List<TamaStat>();
        Eggs = new List<Egg>();
        Buildings = new List<BuildingSaveData>();
        Nickname = "test";
    }
   
    #endregion

    #region Methods
    public void SetNewUser()
    {
        for(int i =0; i< hatchEggsLimitCount; i++)
        {
            Eggs.Add(new Egg());
        }
    }
    public void AddTama()
    {
        TamaStat tama = new TamaStat();
        tama.InitStat();
        Tamas.Add(tama);
        Tama tamas = ObjectPoolManager.instance.GetGo("Tama").GetComponent<Tama>();
        tamas.SetTama(tama);
    }
    public void AddTama(TamaTribe tribe)
    {
        TamaStat tama = new TamaStat();
        tama.InitStat(tribe);
        Tamas.Add(tama);
        Tama tamas = ObjectPoolManager.instance.GetGo("Tama").GetComponent<Tama>();
        tamas.SetTama(tama);
    }

    public void SetUser(User user)
    {
        Inventory = user.Inventory;
        Buildings = user.Buildings;
        Nickname = user.Nickname;
        Tamas.Clear();
        foreach (var data in user.Tamas)
        {  
            Tamas.Add(data);
        }
        for(int i =0;i< hatchEggsLimitCount; i++)
        {
            Eggs[i] = user.Eggs[i];
        }
        HatchingManager.Instance.ReductionHatchingTime();
    }
    #endregion
}