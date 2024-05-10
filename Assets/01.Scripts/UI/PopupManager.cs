/*
 * ���ϸ� : PopupManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ��ü �˾��� �����ϴ� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    #region Variables
    [HideInInspector] public SettingPopup settingPopup;
    [HideInInspector] public EggHatchPopup eggHatchPopup;
    [HideInInspector] public InventoryPopup inventoryPopup;
    [HideInInspector] public ItemPopup itemPopup;
    [HideInInspector] public EggStatePopup eggStatePopup;
    List<BasePopup> popups = new List<BasePopup>();
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
    void Initialize()
    {
        settingPopup = CreatePopUp<SettingPopup>();
        eggHatchPopup = CreatePopUp<EggHatchPopup>();
        inventoryPopup = CreatePopUp<InventoryPopup>();
        itemPopup = CreatePopUp<ItemPopup>();
        eggStatePopup = CreatePopUp<EggStatePopup>();
        foreach (var popup in popups)
            popup.Initialize();
    }

    T CreatePopUp<T>()
    {
        GameObject obj = Resources.Load("Prefabs/UI/Popup/" + typeof(T)) as GameObject;
        if (obj == null)
            obj = Resources.Load("Prefabs/UI/Utils/" + typeof(T)) as GameObject;
        if (obj == null)
        {
            Debug.Log("Empty PopUp UI typeof : " + typeof(T));
            return default(T);
        }
        else
        {
            obj = Instantiate(obj, this.transform);
            popups.Add(obj.GetComponent<BasePopup>());

            return obj.GetComponent<T>();
        }
    }
    #endregion
}