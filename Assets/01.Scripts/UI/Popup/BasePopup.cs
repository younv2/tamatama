/*
 * ���ϸ� : BasePopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ��ü���� �˾��� �⺻ ���̽� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�(����Ű��� ����)
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
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