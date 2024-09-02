/*
 * 파일명 : Inventory.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 유저가 소지한 아이템 리스트 데이터 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/7 - 프로퍼티가 저장이 되지 않는 것을 확인하여 변수로 나눔
 * 2024/5/11 - 아이템 복수 추가 함수 구현
 * 2024/9/3 - 재화 관련 이벤트 추가, 재화에 변동이 있을 경우 UI에 변화가 되도록 작업
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
            Debug.Log("사용됨, 갯수 0개로 인하여 삭제");
        }
    }
    #endregion
}
