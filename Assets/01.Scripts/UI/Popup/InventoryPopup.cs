/*
 * 파일명 : InventoryPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 현재 플레이어가 가지고 있는 아이템 리스트의 정보를 UI로 표시해주는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/28 - 인벤토리 아이템이 소모되어 사라질경우 아이템리스트 전체가 사라지는 버그 수정(아이템리스트의 얕은 복사로 인함)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPopup : BasePopup, IPointerClickHandler
{
    #region Variables
    private int maxSlots = 100;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<ItemSlotUI> slots = new List<ItemSlotUI>();
    private List<Item> userItemList;
    private List<Item> viewItemList = new List<Item>();
    private int viewItemFirstIndex;
    public Action updateSlotAction;
    #endregion

    #region Methods
    public override void Initialize()
    {
        base.Initialize();
        CreateSlots();
        userItemList = GameManager.Instance.user.Inventory.ItemList;
    }
    public void CreateSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(Instantiate(slotPrefab, slotParent).GetComponent<ItemSlotUI>());
        }
        foreach (var slot in slots)
        {
            slot.Init();
            slot.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemSlotUI temp = eventData.pointerEnter.GetComponent<ItemSlotUI>();

        if (temp != null)
        {
            slots[viewItemList.Count].gameObject.SetActive(false);
            UIManager.Instance.itemPopup.Show(temp.Index);
        }
    }
    public void Show(Type type = null)
    {
        base.Show();
        viewItemList.Clear();
        userItemList = GameManager.Instance.user.Inventory.ItemList;
        userItemList.Sort((Item a, Item b) => a.Id.CompareTo(b.Id));
        //타입이 정해져 있지 않을 경우 전체 아이템 출력
        viewItemList =  type == null ?
            new List<Item>(userItemList) : userItemList.Where(x=>x.GetType() == type).ToList();
        viewItemFirstIndex = type == null ?
            0 :  userItemList.FindIndex(x => x.GetType() == type);

        for (int i = 0; i < viewItemList.Count; i++)
        {
            slots[i].SetSlotIndex(viewItemFirstIndex+ i);
            slots[i].SetItem(viewItemList[i].SpritePath, viewItemList[i] is CountableItem item ? item.Amount : 1);
        }
    }
    public override void Close()
    {
        base.Close();

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].gameObject.activeSelf)
                slots[i].gameObject.SetActive(false);
            else
                break;
        }
    }
    #endregion
}
