/*
 * ���ϸ� : BuildingShopElementUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/6/10
 * ���� ������ : 2024/6/10
 * ���� ���� : �ǹ� ���� ������ Ŭ����
 * ���� ���� :
 * 2024/6/10 - �ǹ� ���� ������ ��ũ��Ʈ �ۼ�
 * 2024/8/18 - �����ϱ� ��ư�� ���� �� ������ ����ǵ��� ����
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
