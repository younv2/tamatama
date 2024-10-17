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
 * 2024/10/4 - IAttackable 추가 및 공격자 정보를 같이 넘기도록 수정
 */
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour, IDamageable, IAttackable
{
    [SerializeField] public TamaStat stat;
    private EquipmentManager equipmentManager;
    private HealthManager healthManager;
    private TargetManager targetManager;

    private CombatManager combatManager;
    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    private void Start()
    {
        InitializeManagers();
        ResetStats();
    }
    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        this.stat.SetLevelByStats(stat.Level);
        Debug.Log("Setted Tama Data");
    }
    private void InitializeManagers()
    {
        healthManager = gameObject.AddComponent<HealthManager>();
        targetManager = gameObject.AddComponent<TargetManager>();
        combatManager = gameObject.AddComponent<CombatManager>();

        attackComponent = gameObject.AddComponent<AttackComponent>();
        moveComponent = gameObject.AddComponent<MoveComponent>();

        combatManager.Initialize(attackComponent);
        moveComponent.Initialize(stat.MoveSpeed);
    }

    private void ResetStats()
    {
        healthManager.Initialize(stat.MaxHp);
    }

    private void Update()
    {
        HandleCombat();
    }

    private void HandleCombat()
    {
        Transform target = targetManager.GetTarget();  // 타겟 정보 가져오기

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (combatManager.CanAttack())
            {
                if (distanceToTarget <= stat.AttackRange)
                {
                    combatManager.Attack(target,stat.AttackPower, stat.AttackSpeed,this);  // 타겟을 전달하여 공격 수행
                    moveComponent.StopMove();
                }
                else
                {
                    moveComponent.MoveTo(target.position);  // 타겟을 향해 이동
                }
            }
        }
    }
    public void TakeDamage(int damage, IAttackable attacker,bool isCritical)
    {
        healthManager.TakeDamage(damage, isCritical);
    }
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
    public void GainExp(int exp)
    {
        stat.Exp += exp;
        if(stat.Exp>=stat.MaxExp)
        {
            LevelUp(stat.Level + 1);
        }
    }

    public void GainGold(int gold)
    {
        GameManager.Instance.user.Inventory.EarnGold(gold);
        Debug.Log($"지금 골드 : {GameManager.Instance.user.Inventory.Gold}");
    }
    public void LevelUp(int level)
    {
        //Todo - 레벨업 이펙트 추가
        stat.SetLevelByStats(level);
        stat.Exp = 0;
    }

    
}