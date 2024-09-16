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
    public void TakeDamage(float damage);
}
public interface IAttackable
{
    public void Attack();
}
public interface IAnimationController
{
    void PlayMoveAnimation();
    void PlayIdleAnimation();
}