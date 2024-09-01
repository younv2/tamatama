/*
 * 파일명 : BuildManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/5/29
 * 최종 수정일 : 2024/5/29
 * 파일 설명 : 건물 저장 데이터 클래스
 * 수정 내용 :
 * 2024/6/8 - 건물 데이터를 저장하기 위한 클래스
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
