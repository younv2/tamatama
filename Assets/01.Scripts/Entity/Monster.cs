/*
 * 파일명 : Monster.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/9/25
 * 파일 설명 : 몬스터 스크립트
 * 수정 내용 :
 * 2024/7/2 - 스크립트 작성
 * 2024/9/16 - CurHp 추가
 * 2024/9/25 - Idamagealbe 수정(Die,IsDead 관련 작업)
 */
using System;
using UnityEngine;
public class Monster : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    public static Action<Monster> OnMonsterDeath;
    private MonsterData monsterData;
    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    public float MoveSpeed => throw new System.NotImplementedException();

    public int CurHp { get; set; }
    public bool isDead {  get; set; }
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
        isDead = false;
        monsterData = data;
        CurHp = data.maxHp;
        Debug.Log("Setted Monster Data");
    }

    public void TakeDamage(float damage)
    {
        CurHp -= (int)damage;
        Debug.Log($"{monsterData.name}의 현재 남은 체력 : {CurHp}");
        if (CurHp <= 0)
        {
            Die();
            OnMonsterDeath?.Invoke(this);
        }
    }

    public void Die()
    {
        Debug.Log($"{monsterData.monsterName} has died!");
        // 죽었을 때 처리 로직
        isDead = true;
        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        attackComponent.SetTarget(target);
    }

    public double GetAttackRange()
    {
        return monsterData.attackRange;
    }

    public double GetAttackSpeed()
    {
        return monsterData.attackSpeed;
    }

    public bool IsDead()
    {
        return isDead;
    }
    #endregion
}
