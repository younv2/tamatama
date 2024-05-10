/*
 * ���ϸ� : EggHatchSlot.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/6
 * ���� ���� : �� ��Ī �˾������� �� ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ� �� �� �⺻ ����
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/6 - OnclickSetting���� State�� HATCHED�� ��쿡 ���� �ڵ� �߰� �� SetData ����
 */

using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HatchState { EMPTY, HATCHING, HATCHED }
public class EggHatchSlot : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI contentTxt;

    private EggHatchPopup eggHatchPopup;
    private EggStatePopup eggStatePopup;
    private Button slotBtn;
    #endregion

    #region Properties
    public HatchState State { get; set; }
    [SerializeField] public int Index{ get; set; }
    #endregion

    #region Methods
    private void Start()
    {
        slotBtn = GetComponent<Button>();
        
        eggHatchPopup = PopupManager.Instance.eggHatchPopup.GetComponent<EggHatchPopup>();
        eggStatePopup = PopupManager.Instance.eggStatePopup.GetComponent<EggStatePopup>();

        State = GameManager.Instance.user.Eggs[Index].State;
        OnClickSetting();
    }
    void OnClickSetting()
    {
        slotBtn.onClick.AddListener(() =>
        {
            switch(this.State)
            {
                case HatchState.EMPTY:
                    eggHatchPopup.ShowEggInventory();
                    break;
                case HatchState.HATCHING:
                    eggStatePopup.Show(Index);
                    break;
                case HatchState.HATCHED:
                    eggStatePopup.Show(Index);
                    break;
            }

            eggHatchPopup.ActivedSlotIndex = Index;
        });
    }
    public void SetData()
    {
        if (GameManager.Instance.user.Eggs[Index].RemainTime <= 0 && State == HatchState.HATCHING)
            State = HatchState.HATCHED;
        if (State == HatchState.HATCHING)
        {
            timeTxt.text = TextFormat.SetTimeFormat((int)GameManager.Instance.user.Eggs[Index].RemainTime);
            contentTxt.text = "��ȭ��...";
        }
        else if (State == HatchState.HATCHED)
        {
            contentTxt.text = "��ȭ �Ϸ�!";
            timeTxt.text = string.Empty;
        }
        else if(State == HatchState.EMPTY)
        {
            contentTxt.text = "�� ��ȭ�ϱ�";
            timeTxt.text = "+";
        }
    }
    public void SetState(HatchState hatchState)
    {
        State = hatchState;
    }
    #endregion
}
