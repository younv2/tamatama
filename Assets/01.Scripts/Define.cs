/*
 * 파일명 : Define.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/26
 * 파일 설명 : 전체적인 스크립트에서 사용하는 변수 및 열거자를 정리해둔 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/29 - 타일 타입 추가
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Define
{
    public static float minMapX = -5f;
    public static float maxMapX = 5f;
    public static float minMapY = -5f;
    public static float maxMapY = 5f;

    public enum MapState
    {
        NONE, BUILDING
    }
    public enum TileType { NONE, GREEN, RED, WHITE }

    public enum Sound
    {
        BGM,
        EFFECT,
        MAX_COUNT,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
    }

    public static bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
