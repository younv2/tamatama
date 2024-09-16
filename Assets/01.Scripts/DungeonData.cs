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