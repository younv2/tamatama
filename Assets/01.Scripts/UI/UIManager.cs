/*
 * 파일명 : UIManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 전체 UI을 관리하는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    #region Variables
    [HideInInspector] public SettingPopup settingPopup;
    [HideInInspector] public EggHatchPopup eggHatchPopup;
    [HideInInspector] public InventoryPopup inventoryPopup;
    [HideInInspector] public ItemPopup itemPopup;
    [HideInInspector] public EggStatePopup eggStatePopup;
    [HideInInspector] public DispatchPopup dispatchPopup;
    [HideInInspector] public TamaPopup tamaPopup;

    [HideInInspector] public BuildingShopUI buildingShopUI;

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
        dispatchPopup = CreatePopUp<DispatchPopup>();
        tamaPopup = CreatePopUp<TamaPopup>();
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