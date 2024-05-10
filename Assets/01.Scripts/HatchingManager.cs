/*
 * 파일명 : HatchingManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/24
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 알이 태어나는 것을 관리하는 스크립트
 * 수정 내용 :
 * 2024/4/24 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
public class HatchingManager : MonoSingleton<HatchingManager>
{
    #region Methods
    public void Start()
    {
        ReductionHatchingTime();
    }
    public void ReductionHatchingTime()
    {
        for (int i = 0; i < GameManager.Instance.user.Eggs.Length; i++)
        {
            Egg egg = GameManager.Instance.user.Eggs[i];
            StartCoroutine(egg.StartHatching());
        }
    }
    #endregion
}
