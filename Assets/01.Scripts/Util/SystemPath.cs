/*
 * 파일명 : SystemPath.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 파일이 저장되는 기본 폴더를 가리키기 위한 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using System.IO;
using UnityEngine;

public static class SystemPath
{
    #region Methods
    public static string GetPath(string fileName)
    {
        string path = GetPath();
        return Path.Combine(GetPath(), fileName);
    }

    public static string GetPath()
    {
        string path = null;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(Application.persistentDataPath, "Resources/");
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Resources/");
            case RuntimePlatform.WindowsEditor:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Resources/");
            default:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Resources/");
        }
    }
    #endregion
}