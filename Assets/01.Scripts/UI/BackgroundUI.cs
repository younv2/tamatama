/*
 * ���ϸ� : BackgroundUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� �ִ� UI���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
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
