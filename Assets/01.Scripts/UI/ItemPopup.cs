using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPopup : BasePopup
{
    [Tooltip("������ ������ �̹���")]
    [SerializeField] private Image iconImage;

    [Tooltip("������ ���� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI amountText;


    [Tooltip("������ ��� ��ư")]
    [SerializeField] private Button useBtn;

    [Tooltip("������ �Ǹ� ��ư")]
    [SerializeField] private Button sellBtn;

    private GameObject iconGo;
    private GameObject textGo;

    int itemIndex = -1;
    public bool HasItem => iconImage.sprite != null;

    private void ShowIcon() => iconGo.SetActive(true);
    private void HideIcon() => iconGo.SetActive(false);

    private void ShowText() => textGo.SetActive(true);
    private void HideText() => textGo.SetActive(false);
    public override void Show()
    {
        base.Show();
    }
    public void Show(int itemIndex)
    {
        base.Show();
        this.itemIndex = itemIndex;
    }
    public override void Initialize()
    {
        base.Initialize();

        iconGo = iconImage.gameObject;
        textGo = amountText.gameObject;

        useBtn.onClick.AddListener(() => {
            if(itemIndex != -1)
                GameManager.Instance.user.Inventory.UseItem(itemIndex);
            PopupManager.Instance.eggBornPopup.EggInventoryPopup.updateSlotAction?.Invoke();
            PopupManager.Instance.inventoryPopup.updateSlotAction?.Invoke();
            Close();
        });
        sellBtn.onClick.AddListener(() => { });
        iconGo.SetActive(false);
        textGo.SetActive(false);
    }
    public override void Close()
    {
        base.Close();
        itemIndex = -1;
    }

    public void SetItem(string imagesPath,int amount = 1)
    {
        gameObject.SetActive(true);

        iconImage.sprite = SpriteManager.GetItemSprite(imagesPath);

        ShowIcon();
        if (amount > 1)
        {
            ShowText();
        }
    }
    public void RemoveItem()
    {
        iconImage.sprite = null;
        HideIcon();
        HideText();
    }
}
