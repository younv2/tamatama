/*
 * 파일명 : SpriteManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 이미지들을 관리하기 위한 매니저 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class SpriteManager
{
    #region Variables
    static Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();
    static bool isLoadedSprites = false;
    #endregion

    #region Methods
    public static Sprite GetItemSprite(string key)
    {
        if (itemSprites.ContainsKey(key))
        {
            return itemSprites[key];
        }
        else
        {
            Debug.Log("Null Item Sprite : " + key);
            return null;
        }
    }

    public static void OnLoadAllSprite()
    {
        if (isLoadedSprites)
            return;
        OnLoadSpritesByPath("Items");

        isLoadedSprites = true;
    }

    static void OnLoadSpritesByPath(string _path)
    {
        string path = "Images/" + _path;
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>(path);
        Sprite[] sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(sprites);

        for (int i = 0; i < sprites.Length; i++)
        {
            string str = sprites[i].name.Replace("(Clone)", "");
            switch (_path)
            {
                case "Items":
                    itemSprites.Add(str, sprites[i]);
                    break;
                default:
                    Debug.Log("해당 경로는 존재하지 않습니다.");
                    break;
            }
        }
    }
    #endregion
}