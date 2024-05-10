/*
 * 파일명 : Widget.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : Popup의 기본이 되는 스크립트.
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성(무사키우기 참조)
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
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