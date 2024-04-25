using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    private static Dictionary<Type, Widget> widgets = new Dictionary<Type, Widget>();

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
}