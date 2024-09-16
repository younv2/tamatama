/*
 * 파일명 : TamaLevelStats.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/8/29
 * 최종 수정일 : 2024/8/29
 * 파일 설명 : 종족별 레벨 별 기본 스크립트
 * 수정 내용 :
 * 2024/8/29 - 스크립트 작성
 */

public class TamaLevelStatsData
{
    public int Id { get; }
    public int Lv { get; }
    public int Str { get; }
    public int Dex { get; }
    public int Inteli { get; }
    public int Luck { get; }
    public int Con { get; }
    public int Res { get; }
    public TamaLevelStatsData(int code, int level, int str, int dex, int inteli, int luck, int con, int res)
    {
        Id = code;
        Lv = level;
        Str = str;
        Dex = dex;
        Inteli = inteli;
        Luck = luck;
        Con = con;
        Res = res;
    }
}