/*
 * ���ϸ� : Mob.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/7/2
 * ���� ������ : 2024/7/2
 * ���� ���� : ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/7/2 - ��ũ��Ʈ �ۼ�
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
