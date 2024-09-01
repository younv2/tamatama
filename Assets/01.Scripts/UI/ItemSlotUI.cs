/*
 * ���ϸ� : ItemSlotUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/3
 * ���� ���� : �������� �̹����� ������ ǥ�����ִ� ������ ���� UI ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    #region Variables
    [Tooltip("������ ������ �̹���")]
    [SerializeField] private Image iconImage;

    [Tooltip("������ ���� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI amountText;

    private GameObject iconGo;
    private GameObject textGo;
    #endregion

    #region Properties
    public int Index { get; private set; }
    public bool HasItem => iconImage.sprite != null;
    #endregion

    #region Methods
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

        iconImage.sprite = SpriteManager.GetSprite(imagesPath);
        amountText.text = amount.ToString();

        ShowIcon();
        if (amount > 1)
            ShowText();
        else
            HideText();
    }
    public void RemoveItem()
    {
        iconImage.sprite = null;
        HideIcon();
        HideText();
    }
    #endregion
}
