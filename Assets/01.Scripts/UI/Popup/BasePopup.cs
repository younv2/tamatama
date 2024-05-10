/*
 * 파일명 : BasePopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 전체적인 팝업의 기본 베이스 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성(무사키우기 참조)
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using UnityEngine.UI;

public class BasePopup : Widget
{
    #region Variables
    public Image blockBg = null;

    SimpleButton closeBtn;
    #endregion

    #region Properties
    public SimpleButton CloseBtn { get => closeBtn; }
    #endregion

    #region Methods
    public override void Initialize()
    {
        base.Initialize();

        if (transform.Find("CloseBtn") != null)
        {
            closeBtn = transform.Find("CloseBtn").GetComponent<SimpleButton>();
            closeBtn.OnClick.AddListener(() =>
            {
                //SoundManager.Instance.PlayAudio("UIClick");
                Close();
            });
        }
        Close();
    }

    protected virtual void OnClickEventSetting() { }
    #endregion
}