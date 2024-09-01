/*
 * ���ϸ� : InventoryPopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� �÷��̾ ������ �ִ� ������ ����Ʈ�� ������ UI�� ǥ�����ִ� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/28 - �κ��丮 �������� �Ҹ�Ǿ� �������� �����۸���Ʈ ��ü�� ������� ���� ����(�����۸���Ʈ�� ���� ����� ����)
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
        //Ÿ���� ������ ���� ���� ��� ��ü ������ ���
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
