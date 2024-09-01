/*
 * 파일명 : User.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 유저 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/7 - 프로퍼티가 저장이 되지 않는 것을 확인하여 변수로 나눔
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
 * 2024/6/8 - 건축물 데이터 저장되도록 수정
 * 2024/8/29 - 타마가 종족별로 나올 수 있도록 수정 
 * 2024/9/2 - 부화 관련 데이터 로드가 되지 않는 버그 수정
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