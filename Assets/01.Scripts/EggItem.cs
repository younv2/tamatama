/*
 * 파일명 : EggItem.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/30
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 알 아이템 관련 스크립트
 * 수정 내용 :
 * 2024/4/30 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
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
            Debug.LogWarning("아이템 부족");
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
            Debug.Log("알 사용 완료");
            return true;
        }
    }
    #endregion
}
