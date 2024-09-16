/*
 * 파일명 : DispatchPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/9/3
 * 최종 수정일 : 2024/9/3
 * 파일 설명 : 던전 파견 스크립트
 * 수정 내용 :
 * 2024/9/3 - 스크립트 작성
 * 2024/9/16 - 타마 리스트 추가/ 타마 선택 기능 추가
 * 
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DispatchPopup : BasePopup
{
    #region Variables
    private DungeonData selectedDungeon;
    private List<TamaElementUI> tamaElements = new List<TamaElementUI>();
    [SerializeField] private GameObject tamaElement;
    private List<TamaStat> tamas;
    private List<int> selectedTamaIds = new List<int>();
    [SerializeField] private Button dispatchBtn;
    #endregion

    #region Properties

    #endregion

    #region Methods

    public override void Show()
    {
        base.Show();

        foreach (var data in tamas)
        {
            TamaElementUI temp = Instantiate(tamaElement, transform.Find("Scroll View").Find("Viewport").Find("Content").transform).GetComponent<TamaElementUI>();
            temp.SetUI(data.Id, data.Name);
            tamaElements.Add(temp);
        }
        dispatchBtn.onClick.AddListener(() =>
        {
            if (selectedTamaIds.Count == 0)
            {
                Debug.LogError("선택이 필요합니다.");
                return;
            }
            
            List<TamaStat> tamaStats = GameManager.Instance.user.Tamas.FindAll(x=>selectedTamaIds.Contains(x.Id));
            // 여러 개의 타마 오브젝트를 가져오기
            List<Tama> selectedTamas = tamaStats
                .Select(tamaStat => TamaManager.Instance.GetTama(tamaStat))
                .ToList();

            DungeonManager.Instance.EnterDungeon(selectedTamas, selectedDungeon);

            selectedTamaIds.Clear();


        });
        TamaElementUI.onButtonClicked += SetDispatchTama;

    }
    private void SetDispatchTama(int tamaId)
    {
        if(selectedTamaIds.Exists(x => x == tamaId))
            selectedTamaIds.Remove(tamaId);
        else
            selectedTamaIds.Add(tamaId);
        Debug.Log($"tamaId = {selectedTamaIds.Count}");
    }
    public override void Initialize()
    {
        base.Initialize();

        tamas = GameManager.Instance.user.Tamas;
    }
    public void SetDungeonData(DungeonData dungeonData)
    {
        selectedDungeon = dungeonData;
    }
    public override void Close()
    {
        base.Close();

        //Todo: Destroy가 아닌 방식으로의 개선 예정
        foreach (var data in tamaElements)
        {
            Destroy(data.gameObject);
        }
        TamaElementUI.onButtonClicked -= SetDispatchTama;
        tamaElements.Clear();
    }
    #endregion
}
