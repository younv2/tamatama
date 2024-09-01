/*
 * ���ϸ� : ItemPopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/3
 * ���� ���� : ������ ������ �����ִ� UI ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : BasePopup
{
    #region Variables
    [Tooltip("������ ����")]
    [SerializeField] private ItemSlotUI itemSlot;

    [Tooltip("������ ���� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI descText;

    [Tooltip("������ �̸� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI nameText;

    [Tooltip("������ ��� ��ư")]
    [SerializeField] private Button useBtn;

    [Tooltip("������ �Ǹ� ��ư")]
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
