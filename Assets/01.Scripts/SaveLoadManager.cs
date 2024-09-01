/*
 * 파일명 : SaveLoadManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/24
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 데이터 저장 관리 스크립트
 * 수정 내용 :
 * 2024/4/24 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
 * 2024/9/2 - 부화 관련 데이터 로드가 되지 않는 버그 수정
 */

using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
//Todo : 암호화 관련 작업 추후 작업 참조 : https://glikmakesworld.tistory.com/14
public class SaveLoadManager : MonoBehaviour
{
    #region Variables
    static string userDataFilePath = "user.json";
    #endregion

    #region Methods
    public void Save()
    {
        SaveUser(GameManager.Instance.user);

        Debug.Log("유저 데이터 저장");
    }
    public void Load()
    {
        User user = LoadUser();
        GameManager.Instance.user.SetUser(user);
        foreach(var n in user.Buildings)
        {
            BuildManager.Instance.SetBuildingWithLoadData(n.Id,n.Position);
        }
        UIManager.Instance.eggHatchPopup.InitSlots();

        Debug.Log("유저 데이터 로드");
    }
    public void SaveUser(User user)
    {
        // JsonSerializerSettings 객체를 사용하여 직렬화 설정을 정의할 수 있습니다.
        // TypeNameHandling 설정을 Auto로 설정하여 다형성을 자동으로 처리할 수 있게 합니다.
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        // Json.NET을 사용하여 객체를 JSON 문자열로 직렬화합니다.
        string jsonString = JsonConvert.SerializeObject(user, settings);

        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Create, FileAccess.Write))
        {
            // 파일로 저장할 수 있게 바이트화
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

            // bytes의 내용물을 0 ~ max 길이까지 fs에 복사
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    public User LoadUser()
    {
        string jsonString;
        if(!File.Exists(SystemPath.GetPath(userDataFilePath)))
        {
            Debug.Log("세이브 파일이 존재하지 않음");
            return null;
        }
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
            {
                jsonString = sr.ReadToEnd();
            }
        }

        // JsonSerializerSettings 객체를 사용하여 직렬화 설정을 정의할 수 있습니다.
        // TypeNameHandling 설정을 Auto로 설정하여 다형성을 자동으로 처리할 수 있게 합니다.
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        // Json.NET을 사용하여 JSON 문자열을 User 객체로 역직렬화합니다.
        User user = JsonConvert.DeserializeObject<User>(jsonString, settings);

        return user;
    }
    #endregion
}
