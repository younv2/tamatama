/*
 * 파일명 : TamaStat.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 파일이 저장되는 기본 폴더를 가리키기 위한 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
 */
using Newtonsoft.Json;
using UnityEngine;

//적성 구조체
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
//능력치 랭크
public enum StatRank { NONE = -1, F, E, D, C, B, A, S, MAX_COUNT }
//성격
public enum Personality { NONE = -1,FAITHFUL, MAX_COUNT }
//종족
public enum TamaTribe { NONE = -1,HUMAN,DEVIL,STONE, ELF, MAX_COUNT }
[System.Serializable]
public class TamaStat
{
    #region Variables
    #endregion

    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }                        //이름
    public TamaTribe Tribe { get; set; }                        //종족

    public StatRank StrRank { get; set; }                   // 힘 랭크
    public StatRank DexRank { get; set; }                   // 민첩 랭크
    public StatRank IntRank { get; set; }                   // 지능 랭크
    public StatRank LuckRank { get; set; }                  // 운 랭크
    public StatRank ConRank { get; set; }                   // 체력 랭크
    public StatRank ResRank { get; set ; }                // 인내력 랭크

    public int Level { get; set; }                          // 타마 레벨
    public int Exp { get; set; }                            // 타마 현재 경험치
    public int MaxExp { get; set; }                         // 타마 레벨업에 필요한 경험치
    public Aptitude Aptitude { get; set; }                  // 타마 적성
    public int Stress { get; set; }                         // 타마 스트레스 
    public int MaxHp { get; set; }                          // 타마 최대 체력 
    public int CurHp { get; set; }                          // 타마 현재 체력
    public Personality Personality { get; set; }            // 타마 성격

    [JsonIgnore] public int Strength { get; set; }          // 힘- 전투에서의 공격력, 채광 벌목 등에서의 힘에 사용
    [JsonIgnore] public int Dexterity { get; set; }         //  민첩   - 이동 속도, 채집 및 공격 속도, 회피율 및 치명타에 관여
    [JsonIgnore] public int Intelligence { get; set; }      // 지능 - ?
    [JsonIgnore] public int Luck { get; set; }              // 운 - 회피율 및 치명타에 관여, 채집 및 전투의 드랍율에 관여
    [JsonIgnore] public int Constitution { get; set; }      // 체력 - hp 및 방어력 관여
    [JsonIgnore] public int Resilience { get; set; }         // 인내력 - 스트레스 관여

    [JsonIgnore] public float MoveSpeed { get; set; }         // 이동 속도

    [JsonIgnore] public float AttackSpeed { get; set; }         // 공격 속도
    [JsonIgnore] public float AttackRange  { get; set; }         // 공격 범위
    #endregion

    #region Constructor
    public TamaStat()
    {

    }
    #endregion

    #region Methods
    /*캐릭터를 만들때 처음 스탯 적용*/
    public void SetName(string name)
    { 
        this.Name = name; 
    }
    
    public void InitStat()
    {
        Id = GameManager.Instance.user.Tamas.Count + 1;
        Name = "test";
        Level = 5;
        Exp = 0;
        MoveSpeed = 1;
        SetRandAllStatus();
        Stress = 0;
        MaxHp = 50;
        CurHp = 50;
        Personality = Personality.FAITHFUL;

        Debug.Log("타마 스탯 세팅 완료");
    }
    public void InitStat(TamaTribe tribe)
    {
        Id = GameManager.Instance.user.Tamas.Count + 1;
        Tribe = tribe;
        TamaLevelStatsData statData = DataManager.Instance.FindTamaLevelStatWithId((int)tribe,1);
        Name = "test";
        Level = 1;
        Strength = statData.Str;
        Dexterity = statData.Dex;
        Intelligence = statData.Inteli;
        Luck = statData.Luck;
        Constitution = statData.Con;
        Resilience = statData.Res;
        Exp = 0;
        MoveSpeed = 1;
        SetRandAllStatus();
        SetRandPersonality();
        Stress = 0;
        MaxHp = 50;
        CurHp = 50;

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
    public void SetRandAllStatus(StatRank minRank = 0, StatRank maxRank = StatRank.MAX_COUNT-1)
    {
        StrRank     = GetRandStatRank(minRank, maxRank);
        DexRank     = GetRandStatRank(minRank, maxRank);
        IntRank     = GetRandStatRank(minRank, maxRank);
        LuckRank    = GetRandStatRank(minRank, maxRank);
        ConRank     = GetRandStatRank(minRank, maxRank);
        ResRank   = GetRandStatRank(minRank, maxRank);

        Aptitude = new Aptitude(GetRandStatRank(minRank, maxRank), 
            GetRandStatRank(minRank, maxRank), GetRandStatRank(minRank, maxRank));
    }
    public void SetRandPersonality()
    {
        Personality = (Personality)Random.Range(0, (int)Personality.MAX_COUNT);
    }
    public void AddStress()
    {
        Stress++;
    }
    #endregion
}
