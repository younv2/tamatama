/*
 * 파일명 : AnimationController.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/6
 * 최종 수정일 : 2024/7/6
 * 파일 설명 : 애니메이션 컨트롤러 스크립트 
 * 수정 내용 :
 * 2024/7/6 - 스크립트 작성 
 */

using UnityEngine;

public class AnimationController : IAnimationController
{
    Animator animator;

    public AnimationController(Animator animator)
    {
        this.animator = animator;
    }
    public void PlayIdleAnimation()
    {
        animator.SetBool("isMoving", false);
    }

    public void PlayMoveAnimation()
    {
        animator.SetBool("isMoving", true);
    }
    public void PlayAttackAnimation()
    {
        animator.SetBool("isAttack", true);
    }
}