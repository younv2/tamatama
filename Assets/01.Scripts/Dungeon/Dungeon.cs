/*
* 파일명 : Dungeon.cs
* 작성자 : 윤주호 
* 작성일 : 2024/9/16
* 최종 수정일 : 2024/9/25 
* 파일 설명 : 던전 스크립트
* 수정 내용 : 
* 2024/9/16 : 스크립트 작성, 해당 던전내의 몬스터 리스트 관리 
* 2024/9/25 : 던전의 몬스터 관리를 위한 몬스터 사망 이벤트 처리 작업
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public DungeonData DungeonData { get; set; }
    public List<Monster> activeMonsterLst = new List<Monster>();
    public List<Tama> activeTamaLst = new List<Tama>();
    public bool IsActive = false;

    public void Active()
    {
        IsActive = true;
        Monster.OnMonsterDeath += RemoveMonster; // 이벤트 핸들러 등록
    }

    public void Deactive()
    {
        IsActive = false;
        Monster.OnMonsterDeath -= RemoveMonster; // 이벤트 핸들러 해제
    }

    private void RemoveMonster(Monster monster)
    {
        activeMonsterLst.Remove(monster); // 리스트에서 몬스터 제거
    }
    // 던전에서 몬스터 소환
    public void SpawnMonster(int monsterId, Vector3 position, int count)
    {
        for(int i = 0; i< count; i ++)
        {
            Monster monster = EnemyManager.Instance.SpawnMonsterById(monsterId, position +
                new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
            if (monster != null)
            {
                activeMonsterLst.Add(monster);  // 몬스터를 리스트에 추가
            }
        }

    }
    // 타마 추가
    public void AddTama(Tama tama)
    {
        if (!activeTamaLst.Contains(tama))
        {
            activeTamaLst.Add(tama);
        }
    }

    // 던전 내 모든 몬스터 추적 및 관리
    public void UpdateMonsters()
    {
        if (!IsActive)
            return;
        if(activeMonsterLst.Count <= 0 ) 
        {
            Deactive();
        }
    }
    public void UpdateTamas()
    {
        if (!IsActive)
            return;
    }

    public void InitializeDungeon(DungeonData dungeonData)
    {
        this.DungeonData = dungeonData;
    }
}
