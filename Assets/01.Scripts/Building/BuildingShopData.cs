/*
 * ���ϸ� : BuildingShopData.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/6/10
 * ���� ������ : 2024/6/10
 * ���� ���� : �ǹ� ���� ������ Ŭ����
 * ���� ���� :
 * 2024/6/10 - �ǹ� ���� ������ ��ũ��Ʈ �ۼ�
 */
using UnityEngine;
[System.Serializable]
public class BuildingShopData
{
    private int id;
    private int gold;

    public int Id { get { return id; } }
    public int Gold {  get { return gold; } }
    public Building BuildingData { get; set; }

    public BuildingShopData(int id, int gold)
    {
        this.id = id;
        this.gold = gold;
    }

}
