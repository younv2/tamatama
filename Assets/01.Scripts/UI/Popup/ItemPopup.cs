/*
 * 파일명 : ItemPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 아이템 정보를 보여주는 UI 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : BasePopup
{
    #region Variables
    [Tooltip("아이템 슬롯")]
    [SerializeField] private ItemSlotUI itemSlot;

    [Tooltip("아이템 설명 텍스트")]
    [SerializeField] private TextMeshProUGUI descText;

    [Tooltip("아이템 이름 텍스트")]
    [SerializeField] private TextMeshProUGUI nameText;

    [Tooltip("아이템 사용 버튼")]
    [SerializeField] private Button useBtn;

    [Tooltip("아이템 판매 버튼")]
    [SerializeField] private Button sellBtn;

    int itemIndex = -1;
    #endregion

    #region Methods
    public override void Show()
    {
        base.Show();
    }
    public void Show(int itemIndex)
    {
        base.Show();
        this.itemIndex = itemIndex;
        Item item = GameManager.Instance.user.Inventory.ItemList[itemIndex];
        itemSlot.SetItem(item.SpritePath,item is CountableItem _item ? _item.Amount : 1);
        SetDescription(item.Desc);
        SetItemName(item.Name);
    }
    public override void Initialize()
    {
        base.Initialize();
        itemSlot.Init();
        useBtn.onClick.AddListener(() => {
            if(itemIndex != -1)
                GameManager.Instance.user.Inventory.UseItem(itemIndex);
            UIManager.Instance.eggHatchPopup.EggInventoryPopup.updateSlotAction?.Invoke();
            UIManager.Instance.inventoryPopup.updateSlotAction?.Invoke();
            Close();
        });
        sellBtn.onClick.AddListener(() => { });
    }
    public void SetDescription(string desc)
    {
        descText.text = desc;
    }
    public void SetItemName(string name)
    {
        nameText.text = name;
    }
    public override void Close()
    {
        base.Close();
        itemIndex = -1;
    }
    #endregion
}
