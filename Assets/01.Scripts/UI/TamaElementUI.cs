/*
 * 파일명 : TamaElementUI.cs
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

public class TamaElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tamaNameTxt;
    private int tamaId;
    private Button btn;
    public static event UnityAction<int> onButtonClicked;
    public bool isToggle = false;

    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() => OnButtonClick(tamaId));
    }
    public void SetUI(int tamaId, string tamaName)
    {
        this.tamaId = tamaId;
        tamaNameTxt.text = tamaName;
        Debug.Log(tamaId);
    }
    public void OnButtonClick(int tamaId)
    {
        onButtonClicked?.Invoke(tamaId);
    }

}
