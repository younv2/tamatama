/*
 * ���ϸ� : EggStatePopup.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/5/3
 * ���� ������ : 2024/5/6
 * ���� ���� : �� ��Ī �˾������� �� ������ Ȱ��ȭ �Ǿ����� �� Ŭ�� �� ��� 
 *             ���� Ȱ��ȭ �Ǿ� �ִ� ���� ������ �����ִ� �˾� ��ũ��Ʈ
 * ���� ���� :
 * 2024/5/3 - ��ũ��Ʈ �ۼ� �� ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/6 - �ð� ����, ��� �Ϸ�, �ݱ� ��ư �߰� �� TimeChecker �ڷ�ƾ �߰�, �Ϸ� �Ǿ��� ����� ���� �߰�
 */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggStatePopup : BasePopup
{
    #region Variables
    [SerializeField] private Image eggBackgroundImg;
    [SerializeField] private Image eggImg;

    [SerializeField] private Slider timeSlider;

    [SerializeField] private Button immediatelyCompleteBtn;
    [SerializeField] private Button timeReductionBtn;
    [SerializeField] private Button close2Btn;

    private int index;
    private User user;
    EggHatchPopup eggHatchPopup;
    #endregion

    #region Methods
    public override void Initialize()
    {
        base.Initialize();
        user = GameManager.Instance.user;
        eggHatchPopup = PopupManager.Instance.eggHatchPopup;
        immediatelyCompleteBtn.onClick.AddListener(() => {
            if(eggHatchPopup.Slots[index].State == HatchState.HATCHED)
            {
                eggHatchPopup.Slots[index].State = HatchState.EMPTY;
                eggHatchPopup.Slots[index].SetData();
                TamaStat tamaStat = new TamaStat();

                GameManager.Instance.user.AddTama();
                //Todo : Ÿ�� ���� �� �������� ��������
                Close();
            }
            else
            {
                user.Eggs[index].RemainTime = 0;
            }
        });
        timeReductionBtn.onClick.AddListener(() => {
            if(!user.Eggs[index].IsReduction)
            {
                user.Eggs[index].SetReduction();
                timeReductionBtn.enabled = false;
                timeReductionBtn.interactable = false;
            }
        });
        close2Btn.onClick.AddListener(() => {
            Close();
        });
        
    }

    public void Show(int index)
    {
        base.Show();
        this.index = index;
        StartCoroutine(TimeChecker());
        SetHatchedMenu();
        if (eggHatchPopup.Slots[index].State == HatchState.HATCHED)
        {
            SetHatchedMenu(true);
        }
        if (user.Eggs[this.index].IsReduction)
        {
            timeReductionBtn.interactable = false;
            timeReductionBtn.enabled = false;
        }
        else
        {
            timeReductionBtn.interactable = true;
            timeReductionBtn.enabled = true;
        }
    }
    public override void Close()
    {
        StopCoroutine(TimeChecker());
        base.Close();
    }
    IEnumerator TimeChecker()
    {
        while(true)
        {
            timeSlider.value = (float)(user.Eggs[index].GetRemainTimePer());
            if (eggHatchPopup.Slots[index].State == HatchState.HATCHED)
            {
                SetHatchedMenu(true);
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
    }
    public void SetHatchedMenu(bool isHatched = false)
    {
        timeReductionBtn.gameObject.SetActive(!isHatched);
        TextMeshProUGUI btnText = immediatelyCompleteBtn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        btnText.text = isHatched ? "��ȭ" : "��� �Ϸ�";

    }
    #endregion
}
