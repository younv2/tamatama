/*
 * ���ϸ� : GameManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� �Ѱ� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Variables
    public User user;
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();

        user = new User();
        BackgroundUI.Instance.Init();
        SpriteManager.OnLoadAllSprite();
        Debug.Log("GameManager Awaked");
    }
    private void Start()
    {
        SoundManager soundManager = new SoundManager();
        soundManager.Init();
        user.Inventory.AddItem(DataManager.Instance.FindItemWithId(2001));
        user.Inventory.AddItem(DataManager.Instance.FindItemWithId(1001),5);
        user.Inventory.AddItem(DataManager.Instance.FindItemWithId(1002));
        user.Inventory.AddItem(DataManager.Instance.FindItemWithId(1003));
        user.Inventory.AddItem(DataManager.Instance.FindItemWithId(2002));
    }
    #endregion
}
