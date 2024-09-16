/*
 * 파일명 : DungeonManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/16
 * 최종 수정일 : 2024/9/16
 * 파일 설명 : 던전 관리하는 클래스
 * 수정 내용 :
 * 2024/9/16 - 던전에 타마 입장 관련 메서드 추가
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoSingleton<DungeonManager>
{
    public void EnterDungeon(Tama tama, DungeonData dungeon)
    {
        tama.gameObject.transform.position = dungeon.playerSpawnPoint;
    }

    public void EnterDungeon(List<Tama> tamas, DungeonData dungeon)
    {
        foreach(var tama in tamas)
        {
            tama.gameObject.transform.position = dungeon.playerSpawnPoint + 
                new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f, 1f));
        }

        EnemyManager.Instance.SpawnMonsterById(10001, dungeon.monsterSpawnPoints[0]);
    }
}
