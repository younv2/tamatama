/*
 * 파일명 : BackgroundUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 배경상에 있는 UI관리 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : MonoSingleton<BackgroundUI>
{
    #region Variables
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI cashText;
    private TextMeshProUGUI nameText;

    private Button tamaPopupBtn;
    private Button buildBtn;
    private Button hatchPopupBtn;
    private Button inventoryBtn;
    private Button dispatchPopupBtn;

    private GameObject buildingShopUI;

    User user;
    #endregion

    #region Methods
    public void Init()
    {
        user = GameManager.Instance.user; 
        goldText = transform.Find("Gold").GetChild(0).GetComponent<TextMeshProUGUI>();
        cashText = transform.Find("Cash").GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        buildingShopUI = transform.Find("BuildingShopUI").gameObject;

        tamaPopupBtn = transform.Find("BottomMainBar").Find("TamaPopupBtn").GetComponent<Button>();
        buildBtn = transform.Find("BottomMainBar").Find("BuildBtn").GetComponent<Button>();
        hatchPopupBtn = transform.Find("BottomMainBar").Find("HatchPopupBtn").GetComponent<Button>();
        inventoryBtn = transform.Find("BottomMainBar").Find("InventoryBtn").GetComponent <Button>();
        dispatchPopupBtn = transform.Find("BottomMainBar").Find("DispatchPopupBtn").GetComponent<Button>();

        SettingBtn();
        user.Inventory.OnMoneyChanged += SetCurrentMoneyInUI;

        SetCurrentMoneyInUI();
        nameText.text = user.Nickname.ToString();
    }
    public void SetCurrentMoneyInUI()
    {
        goldText.text = user.Inventory.Gold.ToString();
        cashText.text = user.Inventory.Cash.ToString();
    }
    void SettingBtn()
    {
        tamaPopupBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.tamaPopup.Show();
        });
        buildBtn.onClick.AddListener(() =>
        {
            buildingShopUI.SetActive(true);
        });
        hatchPopupBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.eggHatchPopup.Show();
        });
        inventoryBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.inventoryPopup.Show();
        });
        
        dispatchPopupBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.dungeonPopup.Show();
        });
    }
    #endregion
}
