/*
 * 파일명 : GameManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 게임 총괄 관리 스크립트
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 */

using System.Text.RegularExpressions;
using UnityEngine;
using static Define;

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
        user.Init();
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
