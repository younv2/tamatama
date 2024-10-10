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
 * 2024/10/4 - IAttackable 추가 및 공격자 정보를 같이 넘기도록 수정
 * 2024/10/10 - 타겟에게 움직일 수 있도록 수정
 */
using System;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable, IAttackable
{
    public static Action<Monster> OnMonsterDeath; // 사망 이벤트 선언
    private MonsterData monsterData;

    private IAttackable attacker;

    private HealthManager healthManager;   // 체력 관리
    private CombatManager combatManager;   // 전투 관리
    private TargetManager targetManager;   // 타겟 관리
    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    private void Update()
    {
        HandleCombat();  // 전투 로직 처리
    }

    // 매니저 초기화
    private void InitializeManagers()
    {
        healthManager = gameObject.AddComponent<HealthManager>();
        targetManager = gameObject.AddComponent<TargetManager>();
        combatManager = gameObject.AddComponent<CombatManager>();

        attackComponent = gameObject.AddComponent<AttackComponent>();
        moveComponent = gameObject.AddComponent<MoveComponent>();

        // 매니저 초기화
        combatManager.Initialize(attackComponent);
        moveComponent.Initialize(monsterData.moveSpeed);
    }

    public void SetMonsterData(MonsterData data)
    {
        monsterData = data;
        InitializeManagers();
        healthManager.Initialize(data.maxHp); // 체력 초기화
        Debug.Log("Set Monster Data");
    }

    // 전투 처리
    private void HandleCombat()
    {
        Transform target = targetManager.GetTarget(); // 타겟 가져오기
        if (target == null)
        {
            Debug.Log("타겟이 없습니다.");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (combatManager.CanAttack())
        {
            if (distanceToTarget <= monsterData.attackRange)
            {
                combatManager.Attack(target,monsterData.attackPower, monsterData.attackSpeed,this); // 공격 수행
            }
            else
            {
                moveComponent.MoveTo(target.position); // 타겟을 향해 이동
            }
        }
    }

    public void TakeDamage(int damage, IAttackable attacker, bool isCritical)
    {
        this.attacker = attacker;
        healthManager.TakeDamage(damage, isCritical);
        Debug.Log($"{monsterData.monsterName}의 현재 남은 체력 : {healthManager.CurHp}");

        if (healthManager.IsDead)
        {
            Die();
        }
    }
    public void Die()
    {
        OnMonsterDeath?.Invoke(this); // 사망 이벤트 호출
        if(attacker is Tama)
        {
            Tama tama = attacker as Tama;
            tama.GainExp(monsterData.exp);
            tama.GainGold(monsterData.gold);
        }
        gameObject.SetActive(false);  // 몬스터 비활성화
    }

    // 타겟 관련 메서드
    public void SetTarget(Transform newTarget)
    {
        targetManager.SetTarget(newTarget);
    }

    public Transform GetTarget()
    {
        return targetManager.GetTarget();
    }

    public bool IsDead()
    {
        return healthManager.IsDead;
    }

   
}
