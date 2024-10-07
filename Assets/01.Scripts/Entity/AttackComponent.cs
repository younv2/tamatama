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
 * 2024/10/2 - 공격 속도 및 공격 범위의 경우 타마에서 전부 관리할 수 있도록 수정(실시간 수정이 될 수 있음과, 공격이라는 역할만 주는것이
 * 더욱 효율적일 것으로 사료되어 수정.)
 * 2024/10/6 - 크리티컬 추가
 */
using System;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private AnimationController animationController;

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animationController = new AnimationController(animator);
        }
    }
    // 타겟에 대한 공격을 처리
    public void Attack(Transform attackTarget,int attackPower, IAttackable attacker)
    {
        if (attackTarget != null)
        {
            // 공격 애니메이션 및 데미지 처리
            animationController?.PlayAttackAnimation();
            IDamageable target = attackTarget.GetComponent<IDamageable>();
            bool isCritical = IsCritical(attacker, ref attackPower);
            if (target != null && !target.IsDead())
            {
                target.TakeDamage(attackPower, attacker, isCritical); // 공격 데미지
            }
        }
    }
    public bool IsCritical(IAttackable attacker, ref int attackPower)
    {
        Tama tama = attacker as Tama;
        if(tama != null)
        {
            if(UnityEngine.Random.Range(0f,1f) <= tama.stat.CriticalChance)
            {
                attackPower = (int)Math.Round(attackPower * tama.stat.CriticalDamage);
                return true;
            }
            return false;
        }
        return false;
    }
}