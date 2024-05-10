/*
 * ���ϸ� : Egg.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/11
 * ���� ���� : ������ �������ִ� ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/6 - IsReduction ������Ƽ, SetRecution, GetRemainTimePer �޼��� �߰�  
 * 2024/5/7 - SetRecution�� RemainTime / 2 �� TotalTime / 2�� ���� ,������Ƽ�� ������ ���� �ʴ� ���� Ȯ���Ͽ� ������ ���� 
 * 2024/5/11 - ������ ����� �����ʾ� Newtonsoft.Json���� ���� �� �ش� ���̺귯���� �°� ����
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
            Debug.Log("��Ī �Ϸ�");
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
