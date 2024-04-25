using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour
{
    [Tooltip("������ ������ �̹���")]
    [SerializeField] private Image iconImage;

    [Tooltip("������ ���� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI amountText;

    private GameObject iconGo;
    private GameObject textGo;

    public int Index { get; private set; }
    public bool HasItem => iconImage.sprite != null;
    public void SetSlotIndex(int index) => Index = index;

    private void ShowIcon() => iconGo.SetActive(true);
    private void HideIcon() => iconGo.SetActive(false);

    private void ShowText() => textGo.SetActive(true);
    private void HideText() => textGo.SetActive(false);

    public void Init()
    {
        iconGo = iconImage.gameObject; 
        textGo = amountText.gameObject;

        iconGo.SetActive(false);
        textGo.SetActive(false);
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
