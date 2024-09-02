/*
 * ���ϸ� : EventManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/9/3
 * ���� ������ : 2024/9/3
 * ���� ���� : �̺�Ʈ �߾Ӱ��� ��ũ��Ʈ
 * ���� ���� :
 * 2024/9/3 - ��ũ��Ʈ �ۼ�
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

