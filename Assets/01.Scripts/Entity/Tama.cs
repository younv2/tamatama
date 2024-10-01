/*
 * 파일명 : Tama.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/10/1
 * 파일 설명 : 타마(유저의 캐릭터들)
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/7/3 - 길찾기 알고리즘 추가
 * 2024/7/6 - 애니메이션 관련 추가
 * 2024/7/11 - 컴포넌트 패턴
 * 2024/9/25 - Idamagealbe 수정(Die,IsDead 관련 작업)
 * 2024/9/29 - AttackComponent에 있던 타겟을 Tama스크립트로 이동 및, 실시간으로 이동 및 공격을 할 수 있도록 수정
 * 2024/10/1 - 쿨타임을 받아올 수 있도록 이벤트 구독
 */
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    [SerializeField] private TamaStat stat;

    public int CurHp { get; set; }
    //TamaOutfitPart outfit; //추후 작업

    private AttackComponent attackComponent;
    private MoveComponent moveComponent;
    private Transform target;

    public float MoveSpeed => stat.MoveSpeed;

    public bool isDead { get; set; }

    private bool isOnCooldown = false;
    #endregion

    #region Methods
    private void Start()
    {
        moveComponent = gameObject.AddComponent<MoveComponent>();
        attackComponent = gameObject.AddComponent<AttackComponent>();

        ResetStat();
    }
    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Tama Data");
        isDead = false;
    }
    private void ResetStat()
    {
        moveComponent.Initialize(this);
        attackComponent.Initialize(this);
    }

    public void TakeDamage(float damage)
    {
        CurHp -= (int)damage;
        if (CurHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log($"{stat.Name} has died!");
        // 죽었을 때 처리 로직
        isDead = true;
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        if (!isOnCooldown) // 쿨타임 중이 아니면 공격 가능
        {
            attackComponent.Attack();
        }
        else
        {
            Debug.Log("쿨타임 중입니다. 공격할 수 없습니다.");
        }
    }

    public void MoveTo(Vector3 destination)
    {
        moveComponent.MoveTo(destination);
    }
    public void SetTarget(Transform target)
    {
        
        attackComponent.SetTarget(target);
        this.target = target;
    }
    public Transform GetTarget()
    {
        return attackComponent.GetTarget();
    }

    public double GetAttackRange()
    {
        return stat.AttackRange;
    }

    public double GetAttackSpeed()
    {
        return stat.AttackSpeed;
    }

    public bool IsDead()
    {
        return isDead;
    }
    private void HandleCooldown(bool isOnCooldown)
    {
        this.isOnCooldown = isOnCooldown;
        if (isOnCooldown)
        {
            Debug.Log("타마가 쿨타임 중입니다. 공격 불가능.");
        }
        else
        {
            Debug.Log("쿨타임이 종료되었습니다. 다시 공격 가능.");
        }
    }
    private void Update()
    {
        if (target == null)
            return;

        if (attackComponent.IsTargetInRange())
        {
            moveComponent.StopMove();
            Attack();
            
        }
        else
        {
            Debug.Log($"{target.name} : {target.position}");
            MoveTo(target.position);
        }
    }
    #endregion
}
