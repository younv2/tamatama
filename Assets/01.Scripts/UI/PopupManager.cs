using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoSingleton<PopupManager>
{
    [HideInInspector] public SettingPopup settingPopup;
    [HideInInspector] public EggBornPopup eggBornPopup;
    [HideInInspector] public InventoryPopup inventoryPopup;
    [HideInInspector] public ItemPopup itemPopup;
    List<BasePopup> popups = new List<BasePopup>();

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
    void Initialize()
    {
        settingPopup = CreatePopUp<SettingPopup>();
        eggBornPopup = CreatePopUp<EggBornPopup>();
        inventoryPopup = CreatePopUp<InventoryPopup>();
        itemPopup = CreatePopUp<ItemPopup>();
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
}