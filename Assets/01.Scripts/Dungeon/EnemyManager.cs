/*
 * 파일명 : EnemyManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/16
 * 최종 수정일 : 2024/9/16
 * 파일 설명 :  적 매니저 스크립트
 * 수정 내용 :
 * 2024/9/16 - 스크립트 작성
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyManager : MonoSingleton<EnemyManager>
{
    // 몬스터 아이디로 몬스터를 소환하는 메서드
    public void SpawnMonsterById(int monsterId, Vector3 position,int count = 1)
    {
        // 몬스터 데이터 리스트에서 몬스터 아이디로 찾기
        MonsterData monsterData = DataManager.Instance.MonsterLst.Find(data => data.monsterId == monsterId);

        if (monsterData != null)
        {
            for(int i = 0; i<count; i++)
            // 찾은 몬스터 데이터로 몬스터 소환
            SpawnMonster(monsterData, position);
        }
        else
        {
            Debug.LogError($"Monster with ID {monsterId} not found.");
        }
    }
    public void SpawnMonster(MonsterData monsterData, Vector3 position)
    {
        // Object Pool에서 프리팹을 소환
        GameObject monsterObj = ObjectPoolManager.instance.GetGo(monsterData.prefab.name);

        if (monsterObj != null)
        {
            monsterObj.transform.position = position;

            // Monster 스크립트를 가져와서 MonsterData 설정
            Monster monster = monsterObj.GetComponent<Monster>();
            if (monster != null)
            {
                monster.SetMonsterData(monsterData);
            }

            // 오브젝트 활성화
            monsterObj.SetActive(true);
        }
    }
}
