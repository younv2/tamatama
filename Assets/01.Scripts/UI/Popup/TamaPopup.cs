/*
 * 파일명 : TamaPopup.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/8/25
 * 최종 수정일 : 2024/8/25
 * 파일 설명 : 타마 팝업 스크립트
 * 수정 내용 :
 * 2024/8/25 - 스크립트 작성
 * 2024/8/26 - 타마 Element UI 이벤트 등록
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TamaPopup : BasePopup
{
    private List<TamaStat> tamas;
    [SerializeField] private GameObject tamaElement;
    private List<TamaElementUI> tamaElements =  new List<TamaElementUI>();
    private TextMeshProUGUI tamaTribeTxt;
    private TextMeshProUGUI tamaLevelTxt;
    private TextMeshProUGUI tamaExpTxt;
    private TextMeshProUGUI tamaPersonalityTxt;

    private TextMeshProUGUI tamaStrTxt;
    private TextMeshProUGUI tamaDexTxt;
    private TextMeshProUGUI tamaIntTxt;
    private TextMeshProUGUI tamaLucTxt;
    private TextMeshProUGUI tamaConTxt;
    private TextMeshProUGUI tamaEndTxt;


    [SerializeField] private TextMeshProUGUI tamaNameTxt;

    public override void Show()
    {
        base.Show();

        foreach (var data in tamas)
        {
            TamaElementUI temp = Instantiate(tamaElement, transform.Find("Scroll View").Find("Viewport").Find("Content").transform).GetComponent<TamaElementUI>();
            temp.SetUI(data.Id, data.Name);
            tamaElements.Add(temp);
        }

        TamaElementUI.onButtonClicked += FindTamaWithId;
    }
    public override void Initialize()
    {
        base.Initialize();

        tamas = GameManager.Instance.user.Tamas;
        tamaTribeTxt = transform.Find("TamaInfo").Find("TamaTribe").GetComponent<TextMeshProUGUI>();
        tamaLevelTxt = transform.Find("TamaInfo").Find("TamaLevel").GetComponent<TextMeshProUGUI>();
        tamaExpTxt = transform.Find("TamaInfo").Find("TamaExp").GetComponent<TextMeshProUGUI>();
        tamaPersonalityTxt = transform.Find("TamaInfo").Find("TamaPersonality").GetComponent<TextMeshProUGUI>();

        tamaStrTxt = transform.Find("TamaInfo").Find("Stat").Find("Strength").GetComponent<TextMeshProUGUI>();
        tamaDexTxt = transform.Find("TamaInfo").Find("Stat").Find("Dexterity").GetComponent<TextMeshProUGUI>();
        tamaIntTxt = transform.Find("TamaInfo").Find("Stat").Find("Intelligence").GetComponent<TextMeshProUGUI>();
        tamaLucTxt = transform.Find("TamaInfo").Find("Stat").Find("Luck").GetComponent<TextMeshProUGUI>();
        tamaConTxt = transform.Find("TamaInfo").Find("Stat").Find("Constitution").GetComponent<TextMeshProUGUI>();
        tamaEndTxt = transform.Find("TamaInfo").Find("Stat").Find("Resilience").GetComponent<TextMeshProUGUI>();
    }
    public void FindTamaWithId(int id)
    {
        SetTamaStatus(tamas.Find(x => x.Id == id));
    }
    public void SetTamaStatus(TamaStat tamaStat)
    {
        tamaNameTxt.text = tamaStat.Name;
        tamaTribeTxt.text = tamaStat.Tribe.ToString();
        tamaLevelTxt.text = $"LV.{tamaStat.Level}";
        tamaExpTxt.text = $"{tamaStat.Exp}/{tamaStat.MaxExp} exp";
        tamaPersonalityTxt.text = tamaStat.Personality.ToString();

        tamaStrTxt.text = $"힘 {tamaStat.Strength}({tamaStat.StrRank})";
        tamaDexTxt.text = $"민첩 {tamaStat.Dexterity}({tamaStat.DexRank})";
        tamaIntTxt.text = $"지능 {tamaStat.Intelligence}({tamaStat.IntRank})";
        tamaLucTxt.text = $"운 {tamaStat.Luck}({tamaStat.LuckRank})";
        tamaConTxt.text = $"체력 {tamaStat.Constitution}({tamaStat.ConRank})";
        tamaEndTxt.text = $"인내심 {tamaStat.Resilience}({tamaStat.ResRank})";
    }
    public override void Close()
    {
        base.Close();

        //Todo: Destroy가 아닌 방식으로의 개선 예정
        foreach(var data in tamaElements)
        {
            Destroy(data.gameObject);
        }

        TamaElementUI.onButtonClicked -= FindTamaWithId;
    }

}
