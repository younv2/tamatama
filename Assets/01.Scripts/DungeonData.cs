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

    // ĳ������ ��������Ʈ
    public Vector2 playerSpawnPoint;

    // ���� ��������Ʈ ����Ʈ
    public List<Vector2> monsterSpawnPoints;
}