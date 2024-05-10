/*
 * 파일명 : BackgroundUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 알 해칭 팝업에서의 알 슬롯이 활성화 되어있을 시 클릭 할 경우 
 *             현재 활성화 되어 있는 알의 정보를 보여주는 팝업 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using TMPro;

public class BackgroundUI : MonoSingleton<BackgroundUI>
{
    #region Variables
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI cashText;
    private TextMeshProUGUI nameText;

    User user;
    #endregion

    #region Methods
    public void Init()
    {
        user = GameManager.Instance.user; 
        goldText = transform.Find("Gold").GetChild(0).GetComponent<TextMeshProUGUI>();
        cashText = transform.Find("Cash").GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();

        goldText.text = user.Inventory.Gold.ToString();
        cashText.text = user.Inventory.Cash.ToString();
        nameText.text = user.Nickname.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = user.Inventory.Gold.ToString();
        cashText.text = user.Inventory.Cash.ToString();
        nameText.text = user.Nickname.ToString();
    }
    #endregion
}
