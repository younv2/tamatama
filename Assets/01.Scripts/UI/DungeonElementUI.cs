/*
 * ���ϸ� : DungeonElementUI.cs
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
