/*
 * 파일명 : AttackComponent.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/10/1
 * 파일 설명 : 공격이 가능한 Entity관련 공격 관리 스크립트
 * 수정 내용 :
 * 2024/9/25 - 공격 범위 및 속도에 맞춰 공격할 수 있도록 작업
 * 2024/9/29 - AttackComponent에 있던 타겟을 Tama스크립트로 이동 및, 실시간으로 이동 및 공격을 할 수 있도록 수정
 * 2024/10/1 - Attack에 CoolDown적용
 */
using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private double attackRange;
    private double attackSpeed;
    private Transform attackTarget;
    private AnimationController animationController;

    // 쿨타임 상태를 전달할 Action
    public Action<bool> OnCooldown;


    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animationController = new AnimationController(animator);
        }
    }
    public void Initialize(IAttackable attackableEntity)
    {
        attackRange = attackableEntity.GetAttackRange();
        attackSpeed = attackableEntity.GetAttackSpeed();
    }
    public bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, attackTarget.position) <= attackRange;
    }
    public void Attack()
    {
        if (attackTarget != null)
        {
            animationController?.PlayAttackAnimation();
            IDamageable target = attackTarget.GetComponent<IDamageable>();
            if (target != null&& !target.IsDead())
            {
                target.TakeDamage(5);
                OnCooldown?.Invoke(true); // 쿨타임 시작 알림
                Invoke("EndCooldown", (float)attackSpeed); // 쿨타임 종료 알림
            }
            if (target.IsDead())
            {
                this.attackTarget = null;
            }
        }
    }
    private void EndCooldown()
    {
        OnCooldown?.Invoke(false); // 쿨타임 종료 알림
    }
    public void SetTarget(Transform target)
    {
        this.attackTarget = target;
    }
    public Transform GetTarget()
    {
        return this.attackTarget;
    }
}