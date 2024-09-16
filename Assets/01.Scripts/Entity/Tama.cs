/*
 * ���ϸ� : Tama.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/7/3
 * ���� ���� : Ÿ��(������ ĳ���͵�)
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/7/3 - ��ã�� �˰��� �߰�
 * 2024/7/6 - �ִϸ��̼� ���� �߰�
 * 2024/7/11 - ������Ʈ ����
 */
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    [SerializeField] private TamaStat stat;
    //TamaOutfitPart outfit; //���� �۾�

    private AttackComponent attackComponent;
    private MoveComponent moveComponent;

    public float MoveSpeed => stat.MoveSpeed;
    #endregion

    #region Methods
    private void Start()
    {
        moveComponent = gameObject.AddComponent<MoveComponent>();
        attackComponent = gameObject.AddComponent<AttackComponent>();

        moveComponent.Initialize(this);
        attackComponent.Initialize(this);

        MoveTo(new Vector3(5, 2, 0));
    }
    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Tama Data");
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
    

    public void Attack()
    {
        attackComponent.Attack();
    }

    public void MoveTo(Vector3 destination)
    {
        moveComponent.MoveTo(destination);
    }
    #endregion
}
