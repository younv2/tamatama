/*
 * ���ϸ� : LocalizationTable.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ����ȭ ���̺� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�(����Ű��� ����ȭ ��ũ��Ʈ ����)
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum ELanguage
{
    KR, EN, JP
}

public static class LocalizationTable
{
    #region Variables
    private static readonly Dictionary<string, Dictionary<ELanguage, string>> localizationDict =
        new Dictionary<string, Dictionary<ELanguage, string>>();
    public static ELanguage currentLanguage { get; set; }
    public static Action languageSettings;

    public static bool isInit = false;
    #endregion

    #region Methods
    public static void LoadLanguageSheet()
    {
        var dataList = CSVReader.Read("Language");
        var dict = new Dictionary<string, Dictionary<ELanguage, string>>();
        var langs = Enum.GetValues(typeof(ELanguage));

        var type = typeof(LocalizationTable);
        foreach (var data in dataList)
        {
            try
            {
                var key = data["Key"].ToString();
                dict.Add(key, new Dictionary<ELanguage, string>());

                foreach (var lang in langs)
                {
                    var str = data[lang.ToString()].ToString();

                    dict[key].Add((ELanguage)lang, str);
                }
            }
            catch
            {
                Debug.LogError("Failed set value");
            }
        }

        var field = type.GetField("localizationDict", BindingFlags.Static | BindingFlags.NonPublic);
        if (field == null)
            return;

        field.SetValue(null, dict);
        isInit = true;

        languageSettings?.Invoke();
    }

    public static string Localization(string key, ELanguage lang)
    {
        if (!localizationDict.ContainsKey(key))
            return "";

        if (!localizationDict[key].ContainsKey(lang))
            return "";

        return localizationDict[key][lang];
    }

    public static string Localization(string key)
    {
        if (!localizationDict.ContainsKey(key))
            return key;

        if (!localizationDict[key].ContainsKey(currentLanguage))
            return currentLanguage.ToString();

        return localizationDict[key][currentLanguage];
    }

    public static void SetLanguage(ELanguage lang)
    {
        currentLanguage = lang;
        languageSettings?.Invoke();
        PlayerPrefs.SetString("Language", lang.ToString());
    }
    #endregion
}