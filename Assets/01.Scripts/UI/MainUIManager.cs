using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    private Button settingBtn;
    // Start is called before the first frame update
    void Start()
    {
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        settingBtn.onClick.AddListener(() => { PopupManager.Instance.settingPopup.Show(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
