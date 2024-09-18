
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private float attackRange;
    private Transform attackTarget;
    private AnimationController animationController;

    public void Initialize(IAttackable attackableEntity)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animationController = new AnimationController(animator);
        }
    }
    public void CheckAttackRange()
    {
        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.position) <= attackRange)
        {
            Attack();
        }
    }
    
    public void Attack()
    {
        if (attackTarget != null)
        {
            animationController?.PlayAttackAnimation();
            IDamageable target = attackTarget.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(10); // 예: 10의 데미지를 입힘
            }
        }
    }
    public void SetTarget(Transform target)
    {
        this.attackTarget = target;
    }
}