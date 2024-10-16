/*
 * 파일명 : DungeonManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/16
 * 최종 수정일 : 2024/9/25
 * 파일 설명 : 던전 관리하는 클래스
 * 수정 내용 :
 * 2024/9/16 - 던전에 타마 입장 관련 메서드 추가
 * 2024/9/25 - 던전을 전체적으로 관리하도록 작업 및 던전에 몬스터가 있을 경우 타마가 바로 타겟팅 할 수 있도록 수정
 * 2024/10/10 - 몬스터에게 타마 타겟 설정
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class DungeonManager : MonoSingleton<DungeonManager>
{
    public List<Dungeon> dungeonLst = new List<Dungeon>();

    public void EnterDungeon(Tama tama, DungeonData dungeon)
    {
        tama.gameObject.transform.position = dungeon.playerSpawnPoint;
    }

    public Dungeon CreateDungeon(DungeonData dungeonData)
    {
        Dungeon newDungeon = new Dungeon();
        newDungeon.InitializeDungeon(dungeonData);
        dungeonLst.Add(newDungeon);  
        return newDungeon;
    }

    public void EnterDungeon(List<Tama> tamas, DungeonData dungeonData)
    {
        Dungeon dungeon =  dungeonLst.Find(x=>x.DungeonData  == dungeonData);
        dungeon.Active();
        foreach(var tama in tamas)
        {
            tama.gameObject.transform.position = dungeon.DungeonData.playerSpawnPoint + 
                new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f, 1f));
            dungeon.AddTama(tama);
        }
        dungeon.SpawnMonster(10001, dungeonData.monsterSpawnPoints[0], 20);
    }
    private void Update()
    {
        foreach(var dungeon in dungeonLst)
        {
            if (!dungeon.IsActive)
                continue;
            dungeon.UpdateMonsters();
            dungeon.UpdateTamas();
            foreach(Tama tama in dungeon.activeTamaLst)
            {
                if(dungeon.activeMonsterLst.Count == 0)
                    continue;
                if(tama.GetTarget() != null)
                {
                    continue;
                }
                tama.SetTarget(dungeon.activeMonsterLst.OrderBy(obj => Vector3.Distance(tama.gameObject.transform.position, obj.transform.position))
                            .FirstOrDefault().transform);
            }
            foreach (Monster monster in dungeon.activeMonsterLst)
            {
                if (dungeon.activeTamaLst.Count == 0)
                    continue;
                if (monster.GetTarget() != null)
                {
                    continue;
                }
                monster.SetTarget(dungeon.activeTamaLst.OrderBy(obj => Vector3.Distance(monster.gameObject.transform.position, obj.transform.position))
                            .FirstOrDefault().transform);
            }

        }

    }
}
