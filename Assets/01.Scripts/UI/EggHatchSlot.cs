/*
 * 파일명 : EggHatchSlot.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/6
 * 파일 설명 : 알 해칭 팝업에서의 알 슬롯 관련 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성 및 알 기본 설정
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/6 - OnclickSetting에서 State가 HATCHED일 경우에 대한 코드 추가 및 SetData 수정
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
            contentTxt.text = "부화중...";
        }
        else if (State == HatchState.HATCHED)
        {
            contentTxt.text = "부화 완료!";
            timeTxt.text = string.Empty;
        }
        else if(State == HatchState.EMPTY)
        {
            contentTxt.text = "알 부화하기";
            timeTxt.text = "+";
        }
    }
    public void SetState(HatchState hatchState)
    {
        State = hatchState;
    }
    #endregion
}
