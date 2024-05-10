/*
 * ���ϸ� : PoolAble.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : Ǯ�� ������ Ŭ������ �����ϱ� ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
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