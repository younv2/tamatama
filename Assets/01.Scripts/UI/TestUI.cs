/*
 * 파일명 : TestUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/24
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 프로토 타입 상황 및 빠르게 인게임의 테스트를 위한 UI 스크립트
 * 수정 내용 :
 * 2024/4/24 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class TestUI : MonoBehaviour
{
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AddTamaBtn").GetComponent<Button>().onClick.AddListener(
            () =>
            GameManager.Instance.user.AddTama());
    }
    #endregion
}
