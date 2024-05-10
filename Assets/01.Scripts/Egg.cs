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
using System.Collections;
using UnityEngine;


[System.Serializable]
public class Egg
{
    #region Variables
    #endregion

    #region Properties
    public double RemainTime { get; set; }
    public double TotalTime { get; private set; } = 0;
    public bool IsReduction { get; private set; } = false;
    public HatchState State { get; private set; } = HatchState.EMPTY;
    #endregion

    #region Constructor
    [JsonConstructor]
    public Egg(double remainTime, double totalTime, bool isReduction, HatchState state)
    {
        RemainTime = remainTime;
        TotalTime = totalTime;
        IsReduction = isReduction;
        State = state;
    }
    public Egg()
    {

    }
    #endregion
    #region Methods
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
            State = HatchState.HATCHED;
            Debug.Log("해칭 완료");
            EndHatching();
        }
    }
    public void SetHatchingTime(double time)
    {
        TotalTime = time;
        RemainTime = time;
        State = HatchState.HATCHING;
        IsReduction = false;
    }
    public void EndHatching()
    {
        if (State == HatchState.HATCHED)
            State = HatchState.EMPTY;
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
    #endregion
}
