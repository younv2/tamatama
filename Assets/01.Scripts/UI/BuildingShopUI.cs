/*
 * ���ϸ� : BuildingShopUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/6/10
 * ���� ������ : 2024/8/18
 * ���� ���� : ���� ���� UI
 * ���� ���� :
 * 2024/6/10 - ��ũ��Ʈ �ۼ�
 * 2024/8/18 - �ݱ� ��ư �߰�
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShopUI : MonoSingleton<BuildingShopUI>
{
    private List<BuildingShopElementUI> buildingShopElementUILst = new List<BuildingShopElementUI>();
    [SerializeField] private GameObject slotPrefab;
    private Button closeBtn;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        foreach(var d in DataManager.Instance.buildingShopDataList)
        {
            BuildingShopElementUI temp = Instantiate(slotPrefab, transform.Find("ViewPort").Find("Content").transform).GetComponent<BuildingShopElementUI>();
            temp.SetUI(d.Id, d.Gold);
            buildingShopElementUILst.Add(temp);
        }
        closeBtn.onClick.AddListener(() => { 
            this.gameObject.SetActive(false);
        });
        UIManager.Instance.buildingShopUI = this;
    }
}
