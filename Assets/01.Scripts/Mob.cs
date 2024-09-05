/*
 * 파일명 : Mob.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/7/2
 * 파일 설명 : 몬스터 스크립트
 * 수정 내용 :
 * 2024/7/2 - 스크립트 작성
 */
using UnityEngine;
public class Mob : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    private MobStat stat;
    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    public float MoveSpeed => throw new System.NotImplementedException();

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
    public void SetMob(MobStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Mob Data");
    }

    public void TakeDamage(float damage)
    {
        stat.CurHp -= (int)damage;
    }
    #endregion
}
