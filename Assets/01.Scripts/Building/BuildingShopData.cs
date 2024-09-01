/*
 * 파일명 : BuildingShopData.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/6/10
 * 최종 수정일 : 2024/6/10
 * 파일 설명 : 건물 상점 데이터 클래스
 * 수정 내용 :
 * 2024/6/10 - 건물 상점 데이터 스크립트 작성
 */
using UnityEngine;
[System.Serializable]
public class BuildingShopData
{
    private int id;
    private int gold;

    public int Id { get { return id; } }
    public int Gold {  get { return gold; } }
    public Building BuildingData { get; set; }

    public BuildingShopData(int id, int gold)
    {
        this.id = id;
        this.gold = gold;
    }

}
