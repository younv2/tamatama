using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Aptitude
{
    public Aptitude(StatRank mining, StatRank battle, StatRank farming)
    {
        this.mining = mining;
        this.battle = battle;
        this.farming = farming;
    }
    [SerializeField] StatRank mining;
    [SerializeField] StatRank battle;
    [SerializeField] StatRank farming;
}

public enum StatRank { NONE = -1,F, E, D, C, B, A, S, MAX_COUNT}
public enum Personality { NONE = -1,FAITHFUL, MAX_COUNT }
[System.Serializable]
public class TamaStat
{
    #region Variables

    [SerializeField] string name;                        //타마의 이름

    [SerializeField] StatRank strRank;                   // 힘 랭크
    [SerializeField] StatRank dexRank;　                 // 민첩 랭크
    [SerializeField] StatRank intRank;                   // 지능 랭크
    [SerializeField] StatRank luckRank;                  // 운 랭크
    [SerializeField] StatRank conRank;                   // 체력 랭크
    [SerializeField] StatRank endrnRank;                 // 인내력 랭크

    [SerializeField] int strength;                       // 힘- 전투에서의 공격력, 채광 벌목 등에서의 힘에 사용
    [SerializeField] int dexterity;　                    // 민첩 - 이동 속도, 채집 및 공격 속도, 회피율 및 치명타에 관여
    [SerializeField] int intelligence;                   // 지능 - ?
    [SerializeField] int luck;                           // 운 - 회피율 및 치명타에 관여, 채집 및 전투의 드랍율에 관여
    [SerializeField] int constitution;                   // 체력 - hp 및 방어력 관여
    [SerializeField] int endurance;                      // 인내력 - 스트레스 관여

    [SerializeField] int level;                          // 타마 레벨
    [SerializeField] int exp;                            // 타마 현재 경험치
    [SerializeField] int maxExp;                         // 타마 레벨업에 필요한 경험치
    [SerializeField] Aptitude aptitude;                  // 타마 적성
    [SerializeField] int stress;                         // 타마 스트레스
    [SerializeField] int maxHp;                          // 타마 최대 체력
    [SerializeField] int curHp;                          // 타마 현재 체력
    [SerializeField] Personality personality;            // 타마 성격
    #endregion
    /*캐릭터를 만들때 처음 스탯 적용*/
    /*キャラクターを始めて作る時設定する関数。*/
    public void SetName(string name)
    { 
        this.name = name; 
    }
    public TamaStat()
    {
        
    }
    public void InitStat()
    {
        name = "test";
        level = 1;
        exp = 0;
        SetRandAllStatus();
        stress = 0;
        maxHp = 50;
        curHp = 50;
        personality = Personality.FAITHFUL;

        Debug.Log("타마 스탯 세팅 완료");
    }
    /// <summary>
    /// 스탯 랜덤 지정
    /// </summary>
    StatRank GetRandStatRank(StatRank minRank = 0, StatRank maxRank = StatRank.MAX_COUNT-1)
    {
        return (StatRank)Random.Range((int)minRank, (int)maxRank+1);
    }
    /// <summary>
    /// 전체 스탯 랜덤 지정
    /// </summary>
    void SetRandAllStatus(StatRank minRank = 0, StatRank maxRank = StatRank.MAX_COUNT-1)
    {
        strRank = GetRandStatRank(minRank,maxRank);
        dexRank = GetRandStatRank(minRank,maxRank);
        intRank = GetRandStatRank(minRank, maxRank);
        luckRank = GetRandStatRank(minRank, maxRank);
        conRank = GetRandStatRank(minRank, maxRank);
        endrnRank = GetRandStatRank(minRank, maxRank);

        aptitude = new Aptitude(GetRandStatRank(minRank, maxRank), 
            GetRandStatRank(minRank, maxRank), GetRandStatRank(minRank, maxRank));
    }
    public void AddStress()
    {
        stress++;
    }
}
