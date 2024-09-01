/*
 * ���ϸ� : SpriteManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : �̹������� �����ϱ� ���� �Ŵ��� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class SpriteManager
{
    #region Variables
    static Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();
    static Dictionary<string, Sprite> tileSprites = new Dictionary<string, Sprite>();
    static Dictionary<string, Sprite> buildingSprites = new Dictionary<string, Sprite>();
    static bool isLoadedSprites = false;
    #endregion

    #region Methods
    public static Sprite GetSprite(string key)
    {
        if (itemSprites.ContainsKey(key))
        {
            return itemSprites[key];
        }
        if (tileSprites.ContainsKey(key))
        {
            return tileSprites[key];
        }
        if (buildingSprites.ContainsKey(key))
        {
            return buildingSprites[key];
        }
        else
        {
            Debug.Log("Null Sprite : " + key);
            return null;
        }
    }

    public static void OnLoadAllSprite()
    {
        if (isLoadedSprites)
            return;
        OnLoadSpritesByPath("Items");
        OnLoadSpritesByPath("Tiles");
        OnLoadSpritesByPath("Buildings");
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
                case "Tiles":
                    tileSprites.Add(str, sprites[i]);
                    break;
                case "Buildings":
                    buildingSprites.Add(str, sprites[i]);
                    break;
                default:
                    Debug.Log("�ش� ��δ� �������� �ʽ��ϴ�.");
                    break;
            }
        }
    }
    #endregion
}