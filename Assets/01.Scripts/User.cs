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