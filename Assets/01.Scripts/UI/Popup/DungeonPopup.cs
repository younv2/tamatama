/*
 * 파일명 : DungeonPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/8/18
 * 최종 수정일 : 2024/9/16
 * 파일 설명 : 파견(전투) 팝업 스크립트
 * 수정 내용 :
 * 2024/8/18 - 스크립트 작성
 * 2024/8/25 - 던전 설명UI 추가 
 * 2024/9/16 - 던전 파견 버튼 동작 추가
 */

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonPopup : BasePopup
{
    private List<DungeonElementUI> dungeonElementList = new List<DungeonElementUI>();
    [SerializeField] private TextMeshProUGUI dungeonNameTxt;
    [SerializeField] private TextMeshProUGUI dungeonDescTxt;
    [SerializeField] private GameObject dungeonElement;
    [SerializeField] private Button dispatchBtn;
    private DungeonData currentSelectedDungeon;

    public override void Initialize()
    {
        base.Initialize();

        foreach(var data in DataManager.Instance.DungeonLst)
        {
            DungeonElementUI temp = Instantiate(dungeonElement, transform.Find("Scroll View").Find("Viewport").Find("Content").transform).GetComponent<DungeonElementUI>();
            temp.SetUI(data.dungeonName);
            dungeonElementList.Add(temp);
        }
        dispatchBtn.onClick.AddListener(SetDispatch);
        //첫번째 던전이 바로 선택될 수 있도록 함.
        currentSelectedDungeon = DataManager.Instance.DungeonLst[0];
        SetDungeonDetail();
    }
    void OnEnable()
    {
        DungeonElementUI.OnButtonClicked += HandleButtonClick;
    }

    void OnDisable()
    {
        DungeonElementUI.OnButtonClicked -= HandleButtonClick;
    }
    private void HandleButtonClick(int dungeonCode)
    {
        currentSelectedDungeon = DataManager.Instance.FindDungeonWithId(dungeonCode);
        SetDungeonDetail();
    }
    private void SetDungeonDetail()
    {
        dungeonNameTxt.text = currentSelectedDungeon.dungeonName;
        dungeonDescTxt.text = currentSelectedDungeon.dungeonDesc;
    }
    private void SetDispatch()
    {
        UIManager.Instance.dispatchPopup.SetDungeonData(currentSelectedDungeon);
        UIManager.Instance.dispatchPopup.Show();
    }

}