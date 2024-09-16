/*
 * 파일명 : Tama.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/7/3
 * 파일 설명 : 타마(유저의 캐릭터들)
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/7/3 - 길찾기 알고리즘 추가
 * 2024/7/6 - 애니메이션 관련 추가
 * 2024/7/11 - 컴포넌트 패턴
 */
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour, IMovable, IAttackable, IDamageable
{
    #region Variables
    [SerializeField] private TamaStat stat;
    //TamaOutfitPart outfit; //추후 작업

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
