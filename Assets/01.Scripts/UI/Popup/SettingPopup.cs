using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : BasePopup
{
    private string title;

    private float totalVolume;
    private float FXVolume;
    private float BGVolume;

    private Button logoutBtn;
    public void Init()
    {
        title = LocalizationTable.Localization("SettingPopUp");



    }
}
