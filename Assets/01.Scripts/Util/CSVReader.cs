/*
 * 파일명 : CSVReader.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : csv파일을 읽어오는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    #region Variables
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    #endregion

    #region Methods
    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string[] lines;

        if (File.Exists(SystemPath.GetPath() + file + ".csv"))
        {
            string source;
            StreamReader sr = new StreamReader(SystemPath.GetPath() + file + ".csv");
            source = sr.ReadToEnd();
            sr.Close();

            lines = Regex.Split(source, LINE_SPLIT_RE);

            Debug.Log("Load " + file + ".csv");
        }
        else
        {
            Debug.Log("Failed Load " + file + ".csv file");
            return null;
        }

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                value = value.Replace("<br>", "\n");
                value = value.Replace("<c>", ",");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
    #endregion
}