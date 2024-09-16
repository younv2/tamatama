/*
 * 파일명 : LocalizationTable.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 현지화 테이블 관련 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성(무사키우기 현지화 스크립트 참조)
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
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