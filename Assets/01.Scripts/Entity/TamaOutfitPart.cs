/*
 * 파일명 : SystemPath.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/20
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 타마의 외형 스크립트
 * 수정 내용 :
 * 2024/4/20 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using UnityEngine;

[SerializeField]
public class TamaOutfitPart : MonoBehaviour
{
    #region Properties
    public int FaceId { get; set; }
    public int EyeId{ get;set; }
    public int ClothId{ get;set; }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
