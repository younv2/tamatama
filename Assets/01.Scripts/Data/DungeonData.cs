/*
 * 파일명 : DungeonData.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/16
 * 최종 수정일 : 2024/9/16
 * 파일 설명 : 던전 정보를 저장하는 클래스
 * 수정 내용 :
 * 2024/9/16 - 던전 플레이어블 캐릭터 및 몬스터 스폰 포인트 추가, 스크립터블 오브젝트로 관리하도록 수정
 */
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeon", menuName = "Dungeon/DungeonData")]
public class DungeonData : ScriptableObject
{
    public int dungeonId;
    public string dungeonName;
    public string dungeonDesc;
    public int difficultyLevel;
    public GameObject[] enemies;
    public string[] rewards;

    // 캐릭터의 스폰포인트
    public Vector2 playerSpawnPoint;

    // 몬스터 스폰포인트 리스트
    public List<Vector2> monsterSpawnPoints;
}