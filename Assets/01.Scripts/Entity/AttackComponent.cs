/*
 * 파일명 : AttackComponent.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/9/25
 * 파일 설명 : 공격이 가능한 Entity관련 공격 관리 스크립트
 * 수정 내용 :
 * 2024/9/25 - 공격 범위 및 속도에 맞춰 공격할 수 있도록 작업
 */
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private double attackRange;
    private double attackSpeed;
    private double nextAttackTime; // 다음 공격 가능 시간
    private Transform attackTarget;
    private AnimationController animationController;

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

        nextAttackTime = 0f; // 초기화 시점에 즉시 공격 가능
    }
    public void CheckAttackRange()
    {
        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.position) <= attackRange)
        {
            Attack();
        }
    }
    private bool IsTargetInRange()
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
            }
            if (target.IsDead())
            {
                this.attackTarget = null;
            }
        }
    }
    public void SetTarget(Transform target)
    {
        this.attackTarget = target;
    }
    public Transform GetTarget()
    {
        return this.attackTarget;
    }

    private void Update()
    {
        if (attackTarget == null)
            return;
        this.gameObject.transform.position = attackTarget.position;
        if (Time.time >= nextAttackTime && IsTargetInRange())
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackSpeed; // 다음 공격 시간 설정
        }
    }
}