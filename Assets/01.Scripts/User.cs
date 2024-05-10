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
 */
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    #region Variables
    #endregion

    #region Properties
    public List<TamaStat> Tamas { get; set; }
    public Egg[] Eggs { get; set; }
    public Inventory Inventory { get; set; }
    public string Nickname { get; set; }
    #endregion

    #region Constructor
    public User()
    {
        Inventory = new Inventory();
        Tamas = new List<TamaStat>();
        Eggs = new Egg[3];
        for (int i = 0; i < Eggs.Length; i++)
        {
            Eggs[i] = new Egg();
        }
        Nickname = "test";
        
    }
   
    #endregion

    #region Methods
    public void AddTama()
    {
        TamaStat tama = new TamaStat();
        tama.InitStat();
        Tamas.Add(tama);
    }
    

    public void SetUser(User user)
    {
        Inventory = user.Inventory;
        Nickname = user.Nickname;
        Tamas = user.Tamas;
        Eggs = user.Eggs;
        HatchingManager.Instance.ReductionHatchingTime();
    }
    #endregion
}