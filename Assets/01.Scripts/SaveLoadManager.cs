/*
 * ���ϸ� : SaveLoadManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/24
 * ���� ������ : 2024/5/11
 * ���� ���� : ������ ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/24 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/11 - ������ ����� �����ʾ� Newtonsoft.Json���� ���� �� �ش� ���̺귯���� �°� ����
 * 2024/9/2 - ��ȭ ���� ������ �ε尡 ���� �ʴ� ���� ����
 */

using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
//Todo : ��ȣȭ ���� �۾� ���� �۾� ���� : https://glikmakesworld.tistory.com/14
public class SaveLoadManager : MonoBehaviour
{
    #region Variables
    static string userDataFilePath = "user.json";
    #endregion

    #region Methods
    public void Save()
    {
        SaveUser(GameManager.Instance.user);

        Debug.Log("���� ������ ����");
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

        Debug.Log("���� ������ �ε�");
    }
    public void SaveUser(User user)
    {
        // JsonSerializerSettings ��ü�� ����Ͽ� ����ȭ ������ ������ �� �ֽ��ϴ�.
        // TypeNameHandling ������ Auto�� �����Ͽ� �������� �ڵ����� ó���� �� �ְ� �մϴ�.
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        // Json.NET�� ����Ͽ� ��ü�� JSON ���ڿ��� ����ȭ�մϴ�.
        string jsonString = JsonConvert.SerializeObject(user, settings);

        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Create, FileAccess.Write))
        {
            // ���Ϸ� ������ �� �ְ� ����Ʈȭ
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

            // bytes�� ���빰�� 0 ~ max ���̱��� fs�� ����
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    public User LoadUser()
    {
        string jsonString;
        if(!File.Exists(SystemPath.GetPath(userDataFilePath)))
        {
            Debug.Log("���̺� ������ �������� ����");
            return null;
        }
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
            {
                jsonString = sr.ReadToEnd();
            }
        }

        // JsonSerializerSettings ��ü�� ����Ͽ� ����ȭ ������ ������ �� �ֽ��ϴ�.
        // TypeNameHandling ������ Auto�� �����Ͽ� �������� �ڵ����� ó���� �� �ְ� �մϴ�.
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        // Json.NET�� ����Ͽ� JSON ���ڿ��� User ��ü�� ������ȭ�մϴ�.
        User user = JsonConvert.DeserializeObject<User>(jsonString, settings);

        return user;
    }
    #endregion
}
