/*
 * ���ϸ� : Define.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/26
 * ���� ���� : ��ü���� ��ũ��Ʈ���� ����ϴ� ���� �� �����ڸ� �����ص� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 * 2024/5/29 - Ÿ�� Ÿ�� �߰�
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
        MAX_COUNT,  // �ƹ��͵� �ƴ�. �׳� Sound enum�� ���� ���� ���� �߰�. (0, 1, '2' �̷��� 2��) 
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
