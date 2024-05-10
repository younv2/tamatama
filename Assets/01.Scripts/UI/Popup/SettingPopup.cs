/*
 * 파일명 : SettingPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 게임 설정 UI 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
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
