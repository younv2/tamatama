using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
//Todo : ��ȣȭ ���� �۾� ���� �۾� ���� : https://glikmakesworld.tistory.com/14
public class SaveLoadManager : MonoBehaviour
{
    static string userDataFilePath = "user.json";
    public void Save()
    {
        SaveUser(GameManager.Instance.user);

        Debug.Log("���� ������ ����");
    }
    public void Load()
    {
        User user = LoadUser();
        GameManager.Instance.user.SetUser(user);

        Debug.Log("���� ������ �ε�");
    }
    public void SaveUser(User user)
    {
        string jsonString = JsonUtility.ToJson(user);
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Create, FileAccess.Write))
        {
            //���Ϸ� ������ �� �ְ� ����Ʈȭ
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

            //bytes�� ���빰�� 0 ~ max ���̱��� fs�� ����
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    public User LoadUser()
    {
        if(!File.Exists(SystemPath.GetPath(userDataFilePath)))
        {
            Debug.Log("���̺� ������ �������� ����");
            return null;
        }
        using (FileStream fs = new FileStream(SystemPath.GetPath(userDataFilePath), FileMode.Open, FileAccess.Read))
        {
            //������ ����Ʈȭ ���� �� ���� ������ ����
            byte[] bytes = new byte[(int)fs.Length];

            //���Ͻ�Ʈ������ ���� ����Ʈ ����
            fs.Read(bytes, 0, (int)fs.Length);

            //������ ����Ʈ�� json string���� ���ڵ�
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
