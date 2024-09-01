/*
 * ���ϸ� : EggItem.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/30
 * ���� ������ : 2024/5/3
 * ���� ���� : �� ������ ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/30 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EggItem : CountableItem, IUseable
{
    #region Constructor
    public EggItem(int id, string name, string desc, string spritePath,int maxAmount = 1, int amount = 1) : base(id,name,desc,spritePath,maxAmount,amount)
    {

    }
    [JsonConstructor]
    public EggItem(int id, int amount) : base(id,amount)
    {

    }
    #endregion

    #region Methods
    public bool Use(int amount = 1)
    {
        List<Egg> eggs = GameManager.Instance.user.Eggs;
        int activedEggSlotIndex = UIManager.Instance.eggHatchPopup.ActivedSlotIndex;
        if (Amount - amount < 0)
        {
            Debug.LogWarning("������ ����");
            return false;
        }
        else
        {
            Amount -= amount;
            if(activedEggSlotIndex != -1 && eggs[activedEggSlotIndex].State == HatchState.EMPTY)
            {
                eggs[activedEggSlotIndex].SetTribeFromItemId(Id);
                eggs[activedEggSlotIndex].SetHatchingTime(3);
            }
            Debug.Log("�� ��� �Ϸ�");
            return true;
        }
    }
    #endregion
}
