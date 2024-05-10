/*
 * ���ϸ� : CountableItem.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/30
 * ���� ������ : 2024/5/11
 * ���� ���� : ������ ������ �� �� �ִ� ������ Ŭ����
 * ���� ���� :
 * 2024/4/30 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/11 - ������ ����� �����ʾ� Newtonsoft.Json���� ���� �� �ش� ���̺귯���� �°� ����
 */
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class CountableItem : Item
{
    #region Variables
    #endregion

    #region Properties
    [JsonIgnore] public int MaxAmount { get;}
    [JsonIgnore] public bool IsMax => Amount >= MaxAmount;
    public int Amount { get; set; }
    #endregion

    #region Constructors 
    [JsonConstructor]
    public CountableItem(int id) : base(id)
    {

    }
    [JsonConstructor]
    public CountableItem(int id, int amount) : base(id)
    {
        Amount = amount;
    }
    public CountableItem(int id, string name, string desc, string spritePath, int maxAmount = 1, int amount = 1) : base(id, name, desc, spritePath)
    {
        Amount = amount;
        MaxAmount = maxAmount;
    }
    #endregion

    #region Methods
    
    
    public int AddAmount(int amount = 1)
    {
        int temp = Amount + amount;
        Amount = Mathf.Min(temp, MaxAmount);

        return Mathf.Max(temp - MaxAmount,0);
    }
    #endregion
}
