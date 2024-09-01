/*
 * 파일명 : BuildingShopUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/6/10
 * 최종 수정일 : 2024/8/18
 * 파일 설명 : 건축 상점 UI
 * 수정 내용 :
 * 2024/6/10 - 스크립트 작성
 * 2024/8/18 - 닫기 버튼 추가
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
