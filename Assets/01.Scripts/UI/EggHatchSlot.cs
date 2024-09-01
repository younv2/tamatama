/*
 * ���ϸ� : EggHatchSlot.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/9/2
 * ���� ���� : �� ��Ī �˾������� �� ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/28 - ��ũ��Ʈ �ۼ� �� �� �⺻ ����
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/6 - OnclickSetting���� State�� HATCHED�� ��쿡 ���� �ڵ� �߰� �� SetData ����
 * 2024/9/2 - ��ȭ ���� ������ �ε尡 ���� �ʴ� ���� ����
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
    public void Initialize(int index)
    {
        Index = index;

        slotBtn = GetComponent<Button>();

        eggHatchPopup = UIManager.Instance.eggHatchPopup.GetComponent<EggHatchPopup>();
        eggStatePopup = UIManager.Instance.eggStatePopup.GetComponent<EggStatePopup>();

        GameManager.Instance.user.Eggs[Index].OnHatchStateChanged += HandleStateChange;

        OnClickSetting();
    }
    void OnClickSetting()
    {
        slotBtn.onClick.AddListener(() =>
        {
            switch(State)
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
    private void HandleStateChange(HatchState newState)
    {
        State = newState;
    }
    public void SetUIData()
    { 
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
    #endregion
}
