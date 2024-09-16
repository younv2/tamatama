/*
 * 파일명 : MobStat.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/7/2
 * 파일 설명 : 몹의 능력치를 저장하는 스크립트
 * 수정 내용 :
 * 2024/7/2 - 스크립트 작성
 */
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class MonsterStat
{
    #region Variables
    #endregion

    #region Properties

    public string Name { get; set; }                        //이름

    public int Level { get; set; }                          // 레벨
    public int MaxHp { get; set; }                          // 최대 체력 
    public int CurHp { get; set; }                          // 현재 체력

    public double AttackRange { get; set; }                 // 공격 범위

    public double SearchTargetRange {  get; set; }          // 타겟 탐색 범위
    #endregion

    #region Constructor
    public MonsterStat()
    {

    }
    #endregion

    #region Methods
    /*캐릭터를 만들때 처음 스탯 적용*/
    /*キャラクターを始めて作る時設定する関数。*/
    public void SetName(string name)
    { 
        this.Name = name; 
    }
    
    public void InitStat(string name, int level, int hp, double attackRange, double searchTargetRange)
    {
        Name = name;
        Level = level;
        MaxHp = hp;
        CurHp = hp;
        AttackRange = attackRange;
        SearchTargetRange = searchTargetRange;

        Debug.Log("몹 스탯 세팅 완료");
    }
    #endregion
}
