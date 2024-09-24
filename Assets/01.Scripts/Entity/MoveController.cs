/*
 * 파일명 : MoveController.cs(추후 이름 수정 필요)
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/9/25
 * 파일 설명 : Entity관련 인터페이스 작성
 * 수정 내용 :
 * 2024/9/25 - IAttackable(GetAttackSpeed, GetAttackRange 추가),Idamagealbe 수정(Die,IsDead 관련 작업)
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IMovable
{
    float MoveSpeed { get; }
    void MoveTo(Vector3 destination);
    //void TeleportTo(float x, float y);
}
public interface IDamageable
{
    public bool IsDead();
    public void Die();
    public void TakeDamage(float damage);
}
public interface IAttackable
{
    public void Attack();

    public double GetAttackRange();
    public double GetAttackSpeed();
}
public interface IAnimationController
{
    void PlayMoveAnimation();
    void PlayIdleAnimation();
}