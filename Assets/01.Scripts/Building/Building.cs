/*
 * ���ϸ� : Building.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/5/29
 * ���� ������ : 2024/5/29
 * ���� ���� : �ǹ� Ŭ����
 * ���� ���� :
 * 2024/5/26 - �ǹ� ������ �� ��ġ ������ ���� ���� �ۼ� 
 * 2024/6/8 - �ǹ� �����͸� �����صα� ���� ���� ���� �� ������ ����
 */
using Newtonsoft.Json;
using UnityEngine;
[System.Serializable]
public class Building : MonoBehaviour
{
    [JsonIgnore] public BoundsInt area;
    [JsonIgnore] private new string name;
    [JsonIgnore] private string desc;
    [JsonIgnore] private string prefabName;

    private int id;

    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public string Desc { get { return desc; } }
    public string PrefabName { get { return prefabName; } }
    

    public Building(int id, string name, string desc, string prefabName, int sizeX, int sizeY)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.prefabName = prefabName;
        area.size  = new Vector3Int(sizeX, sizeY, 1);

    }

    public bool Placed { get; private set; }
    // Start is called before the first frame update
    public bool CanbePlaced()
    {
        Vector3Int positionInt = BuildManager.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(BuildManager.Instance.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Place()
    {
        Vector3Int positionInt = BuildManager.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        BuildManager.Instance.TakeArea(areaTemp);
    }
    public void DeepCopy(Building building)
    {
        id = building.id;
        name = building.name;
        desc = building.desc;
        prefabName = building.prefabName;
        area = building.area;
    }
}
