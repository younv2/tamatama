/*
 * ���ϸ� : Inventory.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/11
 * ���� ���� : ������ ������ ������ ����Ʈ ������ ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/7 - ������Ƽ�� ������ ���� �ʴ� ���� Ȯ���Ͽ� ������ ����
 * 2024/5/11 - ������ ���� �߰� �Լ� ����
 * 2024/9/3 - ��ȭ ���� �̺�Ʈ �߰�, ��ȭ�� ������ ���� ��� UI�� ��ȭ�� �ǵ��� �۾�
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    #region Variables
    public event Action OnMoneyChanged;
    #endregion

    #region Properties
    public int Gold { get; set; }
    public int Cash { get; set; }
    public List<Item> ItemList { get; set; } =  new List<Item> { };
    #endregion

    #region Constructor
    public Inventory() 
    {
        Gold = 10000;
        Cash = 500;
    }
    #endregion

    #region Methods
    public void RemoveItem(Item item) => ItemList.Remove(item);
    public void RemoveItem(int index) => ItemList.RemoveAt(index);

    public void AddItem(Item newItem,int amount = 1)
    {
        Item item = Copy.DeepCopy(newItem);
        if (item is not CountableItem)
        {
            ItemList.Add(item);
            return;
        }
        while(amount != 0)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {

                if (ItemList[i] is CountableItem _item && item.Id == _item.Id && !_item.IsMax)
                {
                    amount = _item.AddAmount(amount);
                    if (amount == 0)
                        return;
                }

            }
            item = Copy.DeepCopy(newItem);
            ItemList.Add(item);
            amount--;
        }
        
    }
    public void EarnGold(int amount)
    {
        Gold += amount;
        OnMoneyChanged?.Invoke();
    }
    public void EarnCash(int amount)
    {
        Cash += amount;
        OnMoneyChanged?.Invoke();
    }
    public bool SpendGold(int amount)
    {
        if (Gold < amount)
            return false;
        Gold -= amount;
        OnMoneyChanged?.Invoke();
        return true;
    }
    public bool SpendCash(int amount)
    {
        if (Cash < amount)
            return false;
        Cash -= amount;
        OnMoneyChanged?.Invoke();
        return true;
        
    }
    public bool UseItem(int index, int amount = 1)
    {
        if (ItemList[index] is CountableItem _item && _item is IUseable __item)
        {
            if (__item.Use(amount))
            {
                RemoveItemIfAmountLessThanZero(_item);
                return true;
            }
        }

        return false;
    }
    public bool UseItem(Item item, int amount = 1)
    {
        Item _item = CheckHasItem(item);
        if (_item !=null && _item is CountableItem temp && temp is IUseable __item)
        {
            if (__item.Use(amount))
            {
                RemoveItemIfAmountLessThanZero(temp);
                return true;
            }
        }
        return false;
    }
    public Item CheckHasItem(Item item) => ItemList.Find(x => x == item);

    public void RemoveItemIfAmountLessThanZero(CountableItem item)
    {
        if (item.Amount <= 0)
        {
            RemoveItem(item);
            Debug.Log("����, ���� 0���� ���Ͽ� ����");
        }
    }
    #endregion
}
