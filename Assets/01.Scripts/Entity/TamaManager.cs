/*
* 파일명 : TamaManager.cs
* 작성자 : 윤주호 
* 작성일 : 2024/09/16
* 최종 수정일 : 2024/09/16
* 파일 설명 : 타마 관련 관리하는 매니저 스크립트
* 수정 내용 :
* 2024/09/16 - 유저 스크립트에 있던 타마 추가 등의 기능 분리
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaManager : MonoSingleton<TamaManager>
{
    private Dictionary<TamaStat, Tama> tamaDic = new Dictionary<TamaStat, Tama>();

    // 타마 및 타마스탯 생성
    public TamaStat CreateTama()
    {
        // 새로운 타마 스탯 생성
        TamaStat newTamaStat = new TamaStat();
        newTamaStat.InitStat();

        // 타마 오브젝트 생성 및 매핑
        AddTama(newTamaStat);

        return newTamaStat;
    }

    // 특정 종족의 타마 생성
    public TamaStat CreateTama(TamaTribe tribe)
    {
        // 특정 종족으로 새로운 타마 스탯 생성
        TamaStat newTamaStat = new TamaStat();
        newTamaStat.InitStat(tribe);

        // 타마 오브젝트 생성 및 매핑
        AddTama(newTamaStat);

        return newTamaStat;
    }

    // TamaStat을 받아서 타마 생성 및 매핑
    public void AddTama(TamaStat tamaStat)
    {
        // 타마 오브젝트 생성
        Tama tama = ObjectPoolManager.instance.GetGo("Tama").GetComponent<Tama>();
        tama.SetTama(tamaStat);

        // 타마와 타마스탯 매핑 처리
        if (!tamaDic.ContainsKey(tamaStat))
        {
            tamaDic.Add(tamaStat, tama);
        }
    }

    // 유저의 타마 리스트를 기반으로 동기화
    public void SyncWithUserTamas(List<TamaStat> tamaStats)
    {
        foreach (var tamaStat in tamaStats)
        {
            if (!tamaDic.ContainsKey(tamaStat))
            {
                AddTama(tamaStat);
            }
        }
    }

    // 특정 타마스탯을 가진 타마 오브젝트 찾기
    public Tama GetTama(TamaStat tamaStat)
    {
        if (tamaDic.TryGetValue(tamaStat, out Tama tama))
        {
            return tama;
        }
        return null;
    }
}
