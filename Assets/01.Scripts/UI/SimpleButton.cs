using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButton : Widget
{
    private string languageKey = "";

    [SerializeField] private Image buttonImage = null;
    [SerializeField] private TextMeshProUGUI buttonText = null;
    [SerializeField] protected Button button = null;

    [Header("Components to be used as needed")]
    [SerializeField] private EventTrigger eventTrigger = null;

    public Button.ButtonClickedEvent OnClick => button?.onClick;
    public Image GetImage => buttonImage;
    public TextMeshProUGUI GetText => buttonText;
    public bool SetInteractable { set { button.interactable = value; onInteractableChange?.Invoke(button.interactable); } }
    public Action<bool> onInteractableChange;

    protected virtual void Reset()
    {
        if (buttonImage == null)
            buttonImage = transform.Find("Image").GetComponentInChildren<Image>();

        if (buttonText == null)
        {
            buttonText = transform.Find("Text").GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText == null)
                buttonText = transform.Find("Text (TMP)").GetComponentInChildren<TextMeshProUGUI>();
        }
        if (button == null)
            button = transform.GetComponent<Button>();
    }

    private void Awake()
    {
        LanguageSetting();
    }

    public void SetText(string text) => buttonText.text = text;

    public void Setting(string key, Sprite sprite)
    {
        Setting(sprite);
        Setting(key);
    }

    public void Setting(Sprite sprite) => buttonImage.sprite = sprite;

    public void Setting(string key)
    {
        languageKey = key;
        buttonText.text = LocalizationTable.Localization(languageKey);
    }

    public override void LanguageSetting()
    {
        base.LanguageSetting();

        if (!string.IsNullOrEmpty(languageKey))
            buttonText.text = LocalizationTable.Localization(languageKey);
    }
    public void Active()
    {
        buttonImage.enabled = true;
    }

    public static implicit operator Image(SimpleButton btn) => btn.GetImage;
    public static implicit operator TextMeshProUGUI(SimpleButton btn) => btn.GetText;
    public static implicit operator EventTrigger(SimpleButton btn) => btn.eventTrigger;
}