/*
 * 파일명 : DungeonElementUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/8/18
 * 최종 수정일 : 2024/8/18
 * 파일 설명 : 던전 정보를 보여주는 UI 스크립트
 * 수정 내용 :
 * 2024/8/18 - 스크립트 작성
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DungeonElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DungeonNameTxt;
    private Button btn;
    public static event UnityAction<int> OnButtonClicked;

    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() => OnButtonClick(DataManager.Instance.DungeonLst.Find(x=> DungeonNameTxt.text == x.dungeonName).dungeonId));
    }
    public void SetUI(string dunName)
    {
        DungeonNameTxt.text = dunName;
    }
    public void OnButtonClick(int dungeonId)
    {
        OnButtonClicked?.Invoke(dungeonId);
    }

}
