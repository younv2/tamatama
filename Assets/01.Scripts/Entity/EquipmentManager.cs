/*
 * 파일명 : EquipmentManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/
 * 최종 수정일 : 2024/
 * 파일 설명 :  스크립트
 * 수정 내용 :
 * 2024/
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private Dictionary<EquipmentType, EquipmentItem> equippedItems = new Dictionary<EquipmentType, EquipmentItem>();

    // 장비 장착
    public void EquipItem(EquipmentType slotType, EquipmentItem item)
    {
        if (equippedItems.ContainsKey(slotType))
        {
            equippedItems[slotType] = item;
        }
        else
        {
            equippedItems.Add(slotType, item);
        }
        Debug.Log($"{slotType}에 {item.Name} 장착");
    }

    // 장비 해제
    public void UnequipItem(EquipmentType slotType)
    {
        if (equippedItems.ContainsKey(slotType))
        {
            Debug.Log($"{slotType}에 장착된 {equippedItems[slotType].Name} 장비 해제");
            equippedItems.Remove(slotType);
        }
    }

    // 장비 가져오기
    public EquipmentItem GetEquippedItem(EquipmentType slotType)
    {
        if (equippedItems.ContainsKey(slotType))
        {
            return equippedItems[slotType];
        }
        return null;
    }
}
