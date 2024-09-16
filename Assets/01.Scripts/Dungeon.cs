/*
 * 파일명 : Dungeon.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/8/18
 * 최종 수정일 : 2024/8/18
 * 파일 설명 : 던전 정보를 저장하는 클래스 
 * 수정 내용 :
 * 2024/8/18 - 스크립트 작성
 * 2024/9/16 - 던전 플레이어블 캐릭터 및 몬스터 스폰 포인트 추가
 */

using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private int id;
    private string name;
    private string desc;
    private Vector2 tamaSpawnPoint;
    private List<Vector2> monsterSpawnPointLst;

    public int Id => id;
    public string Name => name;
    public string Desc => desc;
    public Dungeon(int id, string name, string desc,Vector2 tamaSpawnPoint,List<Vector2> monsterSpawnPointLst)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.tamaSpawnPoint = tamaSpawnPoint;
        this.monsterSpawnPointLst = monsterSpawnPointLst;
    }
}