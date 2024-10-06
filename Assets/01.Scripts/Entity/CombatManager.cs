/*
 * 파일명 : CombatManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/2
 * 최종 수정일 : 2024/10/2
 * 파일 설명 : 엔티티의 전투 관리 스크립트
 * 수정 내용 :
 * 2024/10/2 - 엔티티들의 코드 중복 해결 및 유지보수를 위한 세분화 작업
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private AttackComponent attackComponent;
    private float attackCooldown; // 공격 쿨타임 타이머
    private bool isCooldownActive = false; // 쿨타임이 활성화되었는지 여부

    public void Initialize(AttackComponent attackComp)
    {
        attackComponent = attackComp;
    }

    public bool CanAttack()
    {
        // 쿨타임이 비활성화된 경우 공격 가능
        return !isCooldownActive;
    }

    public void Attack(Transform target,int attackPower, float attackSpeed,IAttackable attacker)
    {
        if (target != null && !IsTargetDead(target))
        {
            attackComponent.Attack(target,attackPower,attacker); // 공격 수행

            // 공격 후 쿨타임 시작
            attackCooldown = 1/attackSpeed; // 공격 속도만큼 쿨타임을 설정
            isCooldownActive = true; // 쿨타임 활성화
        }
    }

    private void Update()
    {
        // 쿨타임이 활성화된 경우 쿨타임 감소
        if (isCooldownActive)
        {
            attackCooldown -= Time.deltaTime; // 매 프레임 쿨타임 감소

            // 쿨타임이 완료되면 다시 공격 가능
            if (attackCooldown <= 0f)
            {
                isCooldownActive = false; // 쿨타임 비활성화
            }
        }
    }

    private bool IsTargetDead(Transform target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        return damageable == null || damageable.IsDead();
    }
}
