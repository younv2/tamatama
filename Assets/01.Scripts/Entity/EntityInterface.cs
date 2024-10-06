/*
 * 파일명 : EntityInterface.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/9/25
 * 파일 설명 : Entity관련 인터페이스 작성
 * 수정 내용 :
 * 2024/9/25 - IAttackable(GetAttackSpeed, GetAttackRange 추가),Idamageable 수정(Die,IsDead 관련 작업)
 * 2024/10/4  - IAttackable은 마크 인터페이스로 수정
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IAttackable
{

}

public interface IDamageable
{
    public bool IsDead();
    public void TakeDamage(int damage, IAttackable attacker,bool isCritical);
}

public interface IAnimationController
{
    void PlayMoveAnimation();
    void PlayIdleAnimation();
}