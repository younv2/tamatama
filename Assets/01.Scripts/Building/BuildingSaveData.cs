/*
 * ���ϸ� : BuildManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/5/29
 * ���� ������ : 2024/5/29
 * ���� ���� : �ǹ� ���� ������ Ŭ����
 * ���� ���� :
 * 2024/6/8 - �ǹ� �����͸� �����ϱ� ���� Ŭ����
 */
using Newtonsoft.Json;
using UnityEngine;
[System.Serializable]
public class BuildingSaveData
{
    public int Id { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }

    public Vector3Int Position { get { return new Vector3Int(PosX, PosY, 0); } }

    [JsonConstructor]
    public BuildingSaveData(int id, int posX, int posY)
    {
        Id = id;
        PosX = posX;
        PosY = posY;
    }

    public BuildingSaveData(int id, Vector3 position)
    {
        Id = id;
        PosX = (int)position.x;
        PosY = (int)position.y;
    }
}
