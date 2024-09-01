/*
 * 파일명 : EggHatchPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 알 해칭 팝업에서의 알 슬롯 관련 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성 및 알 기본 설정
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/9/2 - 부화 관련 데이터 로드가 되지 않는 버그 수정
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
