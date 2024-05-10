/*
 * 파일명 : IntroManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 인트로 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoSingleton<IntroManager>
{
    #region Variables
    [SerializeField] private float introTime = 1f;
    #endregion

    #region Methods
    protected override void Awake()
    {
        SetResolution();
        base.Awake();

    }

    private void Start()
    {
        //먼저 처리되어야 할 것들 처리
        Invoke("OnLoadLoginScene", introTime);
    }

    public void SetResolution()
    {
        int setWidth = 1280; // 사용자 설정 너비
        int setHeight = 720; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
    void OnLoadLoginScene()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
}