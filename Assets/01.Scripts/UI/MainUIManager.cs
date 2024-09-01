/*
 * 파일명 : MainUIManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 메인 화면에서의 UI 관련 관리하는 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    #region Variables
    private Button settingBtn;
    // Start is called before the first frame update
    #endregion

    #region Methods
    void Start()
    {
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        settingBtn.onClick.AddListener(() => { UIManager.Instance.settingPopup.Show(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
