/*
 * ���ϸ� : AnimationController.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/7/6
 * ���� ������ : 2024/7/6
 * ���� ���� : �ִϸ��̼� ��Ʈ�ѷ� ��ũ��Ʈ 
 * ���� ���� :
 * 2024/7/6 - ��ũ��Ʈ �ۼ� 
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