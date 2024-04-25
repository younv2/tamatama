using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
//Todo : 암호화 관련 작업 추후 작업 참조 : https://glikmakesworld.tistory.com/14
public class SaveLoadManager : MonoBehaviour
{
    static string userDataFilePath = "user.json";
    public void Save()
    {
        SaveUser(GameManager.Instance.user);

        Debug.Log("유저 데이터 저장");
    }
    public void Load()
    {
        User user = LoadUser();
        GameManager.Instance.user.SetUser(user);

        Debug.Log("유저 데이터 로드");
    }
    public void SaveUser(User user)
    {
        string jsonString = JsonUtility.ToJson(user);
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Create, FileAccess.Write))
        {
            //파일로 저장할 수 있게 바이트화
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

            //bytes의 내용물을 0 ~ max 길이까지 fs에 복사
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    public User LoadUser()
    {
        if(!File.Exists(SystemPath.GetPath(userDataFilePath)))
        {
            Debug.Log("세이브 파일이 존재하지 않음");
            return null;
        }
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Open, FileAccess.Read))
        {
            //파일을 바이트화 했을 때 담을 변수를 제작
            byte[] bytes = new byte[(int)fs.Length];

            //파일스트림으로 부터 바이트 추출
            fs.Read(bytes, 0, (int)fs.Length);

            //추출한 바이트를 json string으로 인코딩
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            User sd = JsonUtility.FromJson<User>(jsonString);
            return sd;
        }
    }
/*    static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "save.json");
    }*/
}
