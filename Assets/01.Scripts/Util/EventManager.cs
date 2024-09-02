/*
 * 파일명 : EventManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/3
 * 최종 수정일 : 2024/9/3
 * 파일 설명 : 이벤트 중앙관리 스크립트
 * 수정 내용 :
 * 2024/9/3 - 스크립트 작성
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    private static Dictionary<string, Action> eventDic = new Dictionary<string, Action>();

    public static void StartListening(string eventName, Action listener)
    {
        if (eventDic.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent += listener;
            eventDic[eventName] = thisEvent;
        }
        else
        {
            eventDic.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (eventDic.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent -= listener;
            eventDic[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventDic.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}

