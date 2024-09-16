/*
 * ���ϸ� : Item.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/11
 * ���� ���� : ������ ���� ��ũ��Ʈ(�ֻ���)
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/4/30 - ������ Ŭ���� ����ȭ
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/11 - ������ ����� �����ʾ� Newtonsoft.Json���� ���� �� �ش� ���̺귯���� �°� ����
 */

using Newtonsoft.Json;
using System.Text.RegularExpressions;

[System.Serializable]
public class Item
{
    #region Variables
    #endregion

    #region Properties
    public int Id { get; set; }
    [JsonIgnore] public string Name { get; }
    [JsonIgnore] public string Desc { get; private set; }
    [JsonIgnore] public string SpritePath { get; }
    #endregion

    #region Constructor
    [JsonConstructor]
    public Item(int id)
    {
        Id = id;
        Item temp = DataManager.Instance.ItemLst.Find(x => x.Id == id); 
        Name = temp.Name;
        Desc = temp.Desc;
        SpritePath = temp.SpritePath;
    }
    public Item(int id, string name, string desc,string spritePath)
    {
        Id = id;
        Name = name;
        Desc = desc;
        SpritePath = spritePath;
    }
    #endregion
}

//����� �� �ִ� ������ �������̽�
public interface IUseable
{
    bool Use(int amount);
}