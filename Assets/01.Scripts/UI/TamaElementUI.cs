/*
 * ���ϸ� : TamaElementUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/8/18
 * ���� ������ : 2024/8/18
 * ���� ���� : ���� ������ �����ִ� UI ��ũ��Ʈ
 * ���� ���� :
 * 2024/8/18 - ��ũ��Ʈ �ۼ�
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
