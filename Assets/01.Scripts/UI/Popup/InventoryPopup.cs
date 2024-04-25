using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : BasePopup, IPointerClickHandler
{
    private int maxSlots = 100;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    List<ItemSlotUI> slots = new List<ItemSlotUI>();
    List<Item> userItemList;
    List<Item> viewItemList;
    int viewItemFirstIndex;
    public Action updateSlotAction;
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
            PopupManager.Instance.itemPopup.Show(temp.Index);
        }
    }
    public void Show(ItemType itemType = ItemType.NONE)
    {
        base.Show();
        //userItemList = userItemList.OrderBy(x => x.Type).ToList();
        userItemList.Sort((Item a, Item b) => a.Type.CompareTo(b.Type));
        //타입이 정해져 있지 않을 경우 전체 아이템 출력
        viewItemList =  itemType == ItemType.NONE ?
            userItemList : userItemList.Where(x=>x.Type == itemType).ToList();
        viewItemFirstIndex = itemType == ItemType.NONE ?
            0 :  userItemList.FindIndex(x => x.Type == itemType);

        for (int i = 0; i < viewItemList.Count; i++)
        {
            slots[i].SetSlotIndex(viewItemFirstIndex+ i);

            slots[i].SetItem(viewItemList[i].SpritePath, viewItemList[i].Amount);
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
}
