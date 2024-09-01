/*
 * 파일명 : BuildingShopElementUI.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/6/10
 * 최종 수정일 : 2024/6/10
 * 파일 설명 : 건물 상점 데이터 클래스
 * 수정 내용 :
 * 2024/6/10 - 건물 상점 데이터 스크립트 작성
 * 2024/8/18 - 건축하기 버튼을 누를 시 상점이 종료되도록 수정
 */
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShopElementUI : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private Button buyBtn;

    public BuildingShopElementUI(int id, int gold)
    {
        Building building = DataManager.Instance.FindBuildingWithId(id);

        nameTxt.text = building.Name;
        itemImage.sprite = SpriteManager.GetSprite(building.PrefabName);
        goldTxt.text = gold.ToString();

        buyBtn.onClick.AddListener(() => {
            BuildManager.Instance.CreateBuilding(building);
        });
    }
    public void SetUI(int id, int gold)
    {
        Building building = DataManager.Instance.FindBuildingWithId(id);

        nameTxt.text = building.Name;
        itemImage.sprite =  SpriteManager.GetSprite(building.PrefabName);
        goldTxt.text = gold.ToString();

        buyBtn.onClick.AddListener(() =>{ 
            BuildManager.Instance.CreateBuilding(building);
            UIManager.Instance.buildingShopUI.gameObject.SetActive(false);
        });
    }

}
