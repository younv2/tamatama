/*
 * 파일명 : MobStat.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/7/2
 * 파일 설명 : 몹의 능력치를 저장하는 스크립트
 * 수정 내용 :
 * 2024/7/2 - 스크립트 작성
 * 2024/9/16 - 스크립터블 오브젝트로 수정
 */
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/MonsterData")]
[System.Serializable]
public class MonsterData : ScriptableObject
{
    #region Variables
    public int monsterId;
    public string monsterName;              // 이름
    public int level;                       // 레벨
    public int maxHp;                       // 최대 체력 
    public int attackPower;                 // 공격력
    public int defence;                     // 방어력
    public float attackSpeed;               // 공격 속도
    public double attackRange;              // 공격 범위
    public double searchTargetRange;        // 타겟 탐색 범위
    public GameObject prefab;
    #endregion
}
