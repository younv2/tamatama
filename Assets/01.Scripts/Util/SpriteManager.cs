using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class SpriteManager
{
    static Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();
    static bool isLoadedSprites = false;

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
}