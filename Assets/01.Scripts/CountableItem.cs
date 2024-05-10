/*
 * 파일명 : CountableItem.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/30
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 아이템 개수를 셀 수 있는 아이템 클래스
 * 수정 내용 :
 * 2024/4/30 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
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
