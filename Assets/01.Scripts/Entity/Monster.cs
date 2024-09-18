/*
 * 파일명 : Monster.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/7/2
 * 파일 설명 : 몬스터 스크립트
 * 수정 내용 :
 * 2024/7/2 - 스크립트 작성
 * 2024/9/16 - CurHp 추가
 */
using UnityEngine;
public class Monster : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    private MonsterData monsterData;
    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    public float MoveSpeed => throw new System.NotImplementedException();

    public int CurHp { get; set; }
    public bool IsDead {  get; set; }
    public void Attack()
    {
        attackComponent.Attack();
    }

    public void MoveTo(Vector3 direction)
    {
        moveComponent.MoveTo(direction);
    }
    #endregion

    #region Methods
    public void SetMonsterData(MonsterData data)
    {
        IsDead = false;
        monsterData = data;
        CurHp = data.maxHp;
        Debug.Log("Setted Monster Data");
    }

    public void TakeDamage(float damage)
    {
        CurHp -= (int)damage;
        if (CurHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{monsterData.monsterName} has died!");
        // 죽었을 때 처리 로직
        IsDead = true;
        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        attackComponent.SetTarget(target);
    }
    #endregion
}
