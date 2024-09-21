/*
* 파일명 : Dungeon.cs
* 작성자 : 윤주호 
* 작성일 : 2024/9/16
* 최종 수정일 : 2024/9/16
* 파일 설명 : 던전 스크립트
* 수정 내용 : 
* 2024/9/16 : 스크립트 작성, 해당 던전내의 몬스터 리스트 관리 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public DungeonData DungeonData { get; set; }
    public List<Monster> activeMonsterLst = new List<Monster>();
    public List<Tama> activeTamaLst = new List<Tama>();

    // 던전에서 몬스터 소환
    public void SpawnMonster(MonsterData monsterData, Vector3 position, int count)
    {
        for(int i = 0; i< count; i ++)
        {
            Monster monster = EnemyManager.Instance.SpawnMonster(monsterData, position +
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
        foreach (var monster in activeMonsterLst)
        {
            // 몬스터 상태 업데이트 (예: 사망 체크)
            if (monster.IsDead)
            {
                activeMonsterLst.Remove(monster);
                // 추가 로직 (예: 몬스터 제거 시 이벤트 처리 등)
            }
        }
    }
    public void UpdateTamas()
    {
        foreach (var tama in activeTamaLst)
        {
            // 몬스터 상태 업데이트 (예: 사망 체크)
            if (tama.IsDead)
            {
                activeTamaLst.Remove(tama);
                // 추가 로직 (예: 몬스터 제거 시 이벤트 처리 등)
            }
        }
    }

    public void InitializeDungeon(DungeonData dungeonData)
    {
        this.DungeonData = dungeonData;
    }
    private void Update()
    {
        Debug.Log("123");
    }
}
