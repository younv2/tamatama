/*
 * 파일명 : EggStatePopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/5/3
 * 최종 수정일 : 2024/5/6
 * 파일 설명 : 알 해칭 팝업에서의 알 슬롯이 활성화 되어있을 시 클릭 할 경우 
 *             현재 활성화 되어 있는 알의 정보를 보여주는 팝업 스크립트
 * 수정 내용 :
 * 2024/5/3 - 스크립트 작성 및 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/6 - 시간 감소, 즉시 완료, 닫기 버튼 추가 및 TimeChecker 코루틴 추가, 완료 되었을 경우의 로직 추가
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
        eggHatchPopup = UIManager.Instance.eggHatchPopup;
        //부화 및 즉시 완료버튼을 누를 경우
        immediatelyCompleteBtn.onClick.AddListener(() => {
            if(eggHatchPopup.Slots[index].State == HatchState.HATCHED)
            {
                user.Eggs[index].Hatching();
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
        SetHatchedMenuUI();
        if (eggHatchPopup.Slots[index].State == HatchState.HATCHED)
        {
            SetHatchedMenuUI(true);
        }
        timeReductionBtn.interactable = !user.Eggs[index].IsReduction;
        timeReductionBtn.enabled = !user.Eggs[index].IsReduction;
    }
    public override void Close()
    {
        StopCoroutine(TimeChecker());
        base.Close();
    }
    //알 상태 팝업의 시간 상태 체크
    IEnumerator TimeChecker()
    {
        while(true)
        {
            timeSlider.value = (float)(user.Eggs[index].GetRemainTimePer());
            if (user.Eggs[index].State == HatchState.HATCHED)
            {
                SetHatchedMenuUI(true);
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
    }
    public void SetHatchedMenuUI(bool isHatched = false)
    {
        timeReductionBtn.gameObject.SetActive(!isHatched);
        TextMeshProUGUI btnText = immediatelyCompleteBtn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        btnText.text = isHatched ? "부화" : "즉시 완료";
    }
    #endregion
}
