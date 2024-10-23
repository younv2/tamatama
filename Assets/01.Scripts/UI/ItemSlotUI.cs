/*
 * 파일명 : ItemSlotUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 아이템의 이미지와 갯수를 표기해주는 아이템 슬롯 UI 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/10/23 - 장착 타입 추가
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    #region Variables
    [Tooltip("장비 아이템 장착칸 ")]
    [SerializeField] private EquipmentType equipmentType;

    [Tooltip("아이템 아이콘 이미지")]
    [SerializeField] private Image iconImage;

    [Tooltip("백그라운드  이미지")]
    [SerializeField] private Image backgroundImage;

    [Tooltip("아이템 개수 텍스트")]
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

        if(equipmentType != EquipmentType.NONE)
        {
            backgroundImage.sprite = SpriteManager.GetSprite("EquipSlotBackground");
        }

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
