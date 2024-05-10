/*
 * 파일명 : PoolAble.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 풀이 가능한 클래스를 구별하기 위해 만든 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using UnityEngine;
using UnityEngine.Pool;

public class PoolAble : MonoBehaviour
{
    #region Properties
    public IObjectPool<GameObject> Pool { get; set; }
    #endregion

    #region Methods
    public void ReleaseObject()
    {
        Pool.Release(gameObject);
    }
    #endregion
}