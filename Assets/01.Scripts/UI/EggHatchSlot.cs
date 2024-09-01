/*
 * 파일명 : EggHatchSlot.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/9/2
 * 파일 설명 : 알 해칭 팝업에서의 알 슬롯 관련 스크립트
 * 수정 내용 :
 * 2024/4/28 - 스크립트 작성 및 알 기본 설정
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/6 - OnclickSetting에서 State가 HATCHED일 경우에 대한 코드 추가 및 SetData 수정
 * 2024/9/2 - 부화 관련 데이터 로드가 되지 않는 버그 수정
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
    #endregion
}
