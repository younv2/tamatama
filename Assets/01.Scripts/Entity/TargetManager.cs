/*
 * 파일명 : TargetManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/2
 * 최종 수정일 : 2024/10/2
 * 파일 설명 : 엔티티의 타겟 관리 스크립트
 * 수정 내용 :
 * 2024/10/2 - 엔티티들의 코드 중복 해결 및 유지보수를 위한 세분화 작업
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public Transform GetTarget()
    {
        if (target == null || IsTargetDead())
            return null;
        return target;
    }

    public bool IsTargetDead()
    {
        if (target == null) return true;

        IDamageable damageable = target.GetComponent<IDamageable>();
        return damageable == null || damageable.IsDead();
    }
}
