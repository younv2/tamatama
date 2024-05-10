/*
 * ���ϸ� : Widget.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : Popup�� �⺻�� �Ǵ� ��ũ��Ʈ.
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�(����Ű��� ����)
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    #region Variables
    private static Dictionary<Type, Widget> widgets = new Dictionary<Type, Widget>();
    #endregion

    #region Methods
    public static T Find<T>() where T : Widget
    {
        return widgets[typeof(T)] as T;
    }

    public virtual void Initialize()
    {
        if (widgets.ContainsKey(GetType()))
            return;
        widgets.Add(GetType(), this);
        LocalizationTable.languageSettings += LanguageSetting;
    }

    public virtual void LanguageSetting()
    {
    }

    public virtual void Show() => gameObject.SetActive(true);

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    #endregion
}