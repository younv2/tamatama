/*
 * 파일명 : Egg.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 유저가 가지고있는 알의 정보 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/6 - IsReduction 프로퍼티, SetRecution, GetRemainTimePer 메서드 추가  
 * 2024/5/7 - SetRecution에 RemainTime / 2 를 TotalTime / 2로 수정 ,프로퍼티가 저장이 되지 않는 것을 확인하여 변수로 나눔 
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
 */

using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class Egg
{
    #region Variables
    public event Action<HatchState> OnHatchStateChanged;
    #endregion

    #region Properties
    public double RemainTime { get; set; }
    public double TotalTime { get; private set; } = 0;
    public bool IsReduction { get; private set; } = false;
    public HatchState State { get; private set; } = HatchState.EMPTY;

    public TamaTribe Tribe { get; private set; } = TamaTribe.NONE ;
    #endregion

    #region Constructor
    [JsonConstructor]
    public Egg(double remainTime, double totalTime, bool isReduction, HatchState state, TamaTribe tribe)
    {
        RemainTime = remainTime;
        TotalTime = totalTime;
        IsReduction = isReduction;
        SetState(state);
        Tribe = tribe;
    }
    public Egg()
    {

    }
    #endregion
    #region Methods
    //해칭중에만 동작할 수 있도록 작업
    public IEnumerator StartHatching()
    {
        while (true)
        {
            yield return new WaitUntil(() => State == HatchState.HATCHING);

            while (RemainTime > 0)
            {
                RemainTime -= 1;
                yield return new WaitForSecondsRealtime(1);
            }
            SetState(HatchState.HATCHED);
            Debug.Log($"해칭 완료");
            
        }
    }
    public void SetHatchingTime(double time)
    {
        TotalTime = time;
        RemainTime = time;
        SetState(HatchState.HATCHING);
        IsReduction = false;
    }
    public void Hatching()
    {
        GameManager.Instance.user.AddTama(Tribe);
        if (State == HatchState.HATCHED)
        {
            SetState(HatchState.EMPTY);
        }
    }
    public void SetReduction()
    {
        IsReduction = true;
        RemainTime -= TotalTime / 2;
        if (RemainTime < 0)
            RemainTime = 0;
    }
    public double GetRemainTimePer()
    {
        return RemainTime / TotalTime;
    }

    internal void SetTribe(int tribeId)
    {
        Tribe = (TamaTribe)tribeId;
    }
    public void SetState(HatchState state)
    {
        if (State != state)
        {
            State = state;
            OnHatchStateChanged?.Invoke(State);
        }
    }
    #endregion
}
