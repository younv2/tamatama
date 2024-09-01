/*
 * ���ϸ� : EggHatchPopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/3
 * ���� ���� : �� ��Ī �˾������� �� ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ� �� �� �⺻ ����
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/9/2 - ��ȭ ���� ������ �ε尡 ���� �ʴ� ���� ����
 */

using System.Collections;
using UnityEngine;

public class EggHatchPopup : BasePopup
{
    #region Variables
    [SerializeField] private GameObject eggInventoryGo;
    [SerializeField] private GameObject slotPrefab;
    private InventoryPopup eggInventoryPopup;
    private EggHatchSlot[] slots;
    private int activedSlotIndex;
    private int maxSlot = 3;
    #endregion

    #region Properties
    public int ActivedSlotIndex { get { return activedSlotIndex; } set { activedSlotIndex = value; } }
    public InventoryPopup EggInventoryPopup { get => eggInventoryPopup; }
    public EggHatchSlot[] Slots { get { return slots; } }
    public int MaxSlot { get => maxSlot; }
    #endregion

    #region Methods
    public override void Initialize()
    {
        eggInventoryPopup = eggInventoryGo.GetComponent<InventoryPopup>();
        eggInventoryPopup.Initialize();
        base.Initialize();
        CreateSlots();
        InitSlots();
        eggInventoryGo.SetActive(false);
    }
    public void CreateSlots()
    {
        slots = new EggHatchSlot[maxSlot];
        for (int i = 0; i < maxSlot; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform.Find("Slots").transform).GetComponent<EggHatchSlot>();
        }
    }
    public void InitSlots()
    {
        for (int i = 0; i < maxSlot; i++)
        {
            slots[i].Initialize(i);
        }
    }

    public override void Show()
    {
        base.Show();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetUIData();
        }

        eggInventoryPopup.updateSlotAction += CloseEggInventory;
        StartCoroutine(UpdateSlotTime());
    }
    public void ShowEggInventory()
    {
        eggInventoryGo.SetActive(true);

        eggInventoryPopup.Show(typeof(EggItem));
    }
    public void CloseEggInventory() => eggInventoryPopup.Close();
    public override void Close()
    {
        base.Close();
        activedSlotIndex = -1;
        CloseEggInventory();
        StopCoroutine(UpdateSlotTime());
        eggInventoryPopup.updateSlotAction -= CloseEggInventory;
    }
    IEnumerator UpdateSlotTime()
    {
        while(true)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetUIData();
            }
            yield return new WaitForSecondsRealtime(1);

            InfiniteLoopDetector.Run();
        }
    }
    #endregion
}
