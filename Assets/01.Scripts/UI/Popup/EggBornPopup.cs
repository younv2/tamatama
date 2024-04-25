using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EggBornPopup : BasePopup
{
    [SerializeField]GameObject eggInventoryGo;
    public InventoryPopup EggInventoryPopup { get => eggInventoryPopup; }
    InventoryPopup eggInventoryPopup;
    public override void Initialize()
    {
        eggInventoryPopup = eggInventoryGo.GetComponent<InventoryPopup>();
        eggInventoryPopup.Initialize();
        base.Initialize();

        eggInventoryGo.SetActive(false);
    }

    public override void Show()
    {
        base.Show();
        eggInventoryPopup.updateSlotAction += CloseEggInventory;
    }
    public void ShowEggInventory()
    {
        eggInventoryGo.SetActive(true);

        eggInventoryPopup.Show(ItemType.EGG);
    }
    public void CloseEggInventory() => eggInventoryPopup.Close();
    public override void Close()
    {
        base.Close();
        CloseEggInventory();
        eggInventoryPopup.updateSlotAction -= CloseEggInventory;
    }
}
