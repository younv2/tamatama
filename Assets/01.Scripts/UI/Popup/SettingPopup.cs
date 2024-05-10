/*
 * ���ϸ� : SettingPopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� ���� UI ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using UnityEngine.UI;

public class SettingPopup : BasePopup
{
    #region Variables
    private string title;

    private float totalVolume;
    private float FXVolume;
    private float BGVolume;

    private Button logoutBtn;
    #endregion

    #region Methods
    public void Init()
    {
        title = LocalizationTable.Localization("SettingPopUp");

    }
    #endregion
}
