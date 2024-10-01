/*
 * 파일명 : HUDObjectPoolManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/1
 * 최종 수정일 : 2024/10/1
 * 파일 설명 : HUD오브젝트 풀을 관리하는 스크립트
 * 수정 내용 :
 * 2024/10/1 - 스크립트 작성
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HUDObjectPoolManager : MonoSingleton<HUDObjectPoolManager>
{
    [System.Serializable]
    private class ObjectInfo
    {
        // 오브젝트 이름
        public string objectName;
        // 오브젝트 풀에서 관리할 오브젝트
        public GameObject perfab;
        // 몇개를 미리 생성 해놓을건지
        public int count;
    }

    #region Variables

    // 오브젝트풀 매니저 준비 완료표시
    public bool IsReady { get; private set; }

    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    // 생성할 오브젝트의 key값지정을 위한 변수
    private string objectName;

    // 오브젝트풀들을 관리할 딕셔너리
    private Dictionary<string, IObjectPool<GameObject>> ojbectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();

    // 오브젝트풀에서 오브젝트를 새로 생성할때 사용할 딕셔너리
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    #endregion

    #region Methods
    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        IsReady = false;

        for (int idx = 0; idx < objectInfos.Length; idx++)
        {
            
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject, true, objectInfos[idx].count, objectInfos[idx].count);

            if (goDic.ContainsKey(objectInfos[idx].objectName))
            {
                Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", objectInfos[idx].objectName);
                return;
            }
            GameObject obj = new GameObject(objectInfos[idx].objectName, typeof(RectTransform));
            obj.transform.parent = transform;
            // 스케일을 명시적으로 설정 (1, 1, 1로)
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            goDic.Add(objectInfos[idx].objectName, objectInfos[idx].perfab);
            ojbectPoolDic.Add(objectInfos[idx].objectName, pool);

            // 미리 오브젝트 생성 해놓기
            for (int i = 0; i < objectInfos[idx].count; i++)
            {
                objectName = objectInfos[idx].objectName;
                PoolAble poolAbleGo = CreatePooledItem(obj.transform).GetComponent<PoolAble>();
                poolAbleGo.ReleaseObject();
            }
        }

        Debug.Log("오브젝트풀링 준비 완료");
        IsReady = true;
    }

    // 생성
    private GameObject CreatePooledItem()
    {
        GameObject obj = new GameObject("name");
        GameObject poolGo = Instantiate(goDic[objectName],obj.transform);
        poolGo.GetComponent<PoolAble>().Pool = ojbectPoolDic[objectName];
        return poolGo;
    }
    private GameObject CreatePooledItem(Transform parent)
    {
        GameObject poolGo = Instantiate(goDic[objectName], parent.transform);
        poolGo.GetComponent<PoolAble>().Pool = ojbectPoolDic[objectName];
        return poolGo;
    }

    // 대여
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetGo(string goName)
    {
        objectName = goName;

        if (goDic.ContainsKey(goName) == false)
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", goName);
            return null;
        }

        return ojbectPoolDic[goName].Get();
    }
    #endregion
}