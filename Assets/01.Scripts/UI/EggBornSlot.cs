using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggBornSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] TextMeshProUGUI contentTxt;

    Button slotBtn;
    bool isActived = false;
    public bool IsActived { get { return isActived; }}

    private void Start()
    {
        slotBtn = GetComponent<Button>();

        OnClickSetting();
    }
    void OnClickSetting()
    {
        slotBtn.onClick.AddListener(() =>
        {
            EggBornPopup eggBornPopup = PopupManager.Instance.eggBornPopup as EggBornPopup;
            eggBornPopup.ShowEggInventory();
        });
    }
}
