using UnityEngine;
using UnityEngine.UI;

public class BasePopup : Widget
{
    public Image blockBg = null;

    SimpleButton closeBtn;

    public SimpleButton CloseBtn { get => closeBtn; }

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
}