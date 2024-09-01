/*
 * ���ϸ� : Pathfinding.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/7/3
 * ���� ������ : 2024/7/3
 * ���� ���� : ��ã�� �˰��� ��ũ��Ʈ
 * ���� ���� :
 * 2024/7/3 - ��ũ��Ʈ �ۼ�
 */

using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.Tilemaps;

public class Pathfinding
{
    private GridLayout gridLayout;
    private Tilemap mainTilemap;
    private Dictionary<TileType, TileBase> tileBases;

    public Pathfinding(GridLayout gridLayout, Tilemap mainTilemap, Dictionary<TileType, TileBase> tileBases)
    {
        this.gridLayout = gridLayout;
        this.mainTilemap = mainTilemap;
        this.tileBases = tileBases;
    }

    public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 targetWorldPos)
    {
        Vector3Int startCell = gridLayout.WorldToCell(startWorldPos);
        Vector3Int targetCell = gridLayout.WorldToCell(targetWorldPos);

        List<Vector3Int> openList = new List<Vector3Int>();
        HashSet<Vector3Int> closedList = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float>();

        openList.Add(startCell);
        gScore[startCell] = 0;
        fScore[startCell] = Heuristic(startCell, targetCell);

        while (openList.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openList, fScore);
            if (current == targetCell)
            {
                return ReconstructPath(cameFrom, current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedList.Contains(neighbor) || !IsWalkable(neighbor))
                {
                    continue;
                }

                float tentativeGScore = gScore[current] + Heuristic(current, neighbor);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetCell);
            }
        }

        return null; // ��θ� ã�� �� ����
    }

    private float Heuristic(Vector3Int a, Vector3Int b)
    {
        return Vector3Int.Distance(a, b);
    }

    private Vector3Int GetLowestFScore(List<Vector3Int> openList, Dictionary<Vector3Int, float> fScore)
    {
        Vector3Int lowest = openList[0];
        foreach (var node in openList)
        {
            if (fScore[node] < fScore[lowest])
            {
                lowest = node;
            }
        }
        return lowest;
    }

    private List<Vector3Int> GetNeighbors(Vector3Int current)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            current + new Vector3Int(1, 0, 0),
            current + new Vector3Int(-1, 0, 0),
            current + new Vector3Int(0, 1, 0),
            current + new Vector3Int(0, -1, 0)
        };

        return neighbors;
    }

    private bool IsWalkable(Vector3Int cell)
    {
        TileBase tile = mainTilemap.GetTile(cell);
        return tile == tileBases[TileType.WHITE];
    }

    private List<Vector3> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3> totalPath = new List<Vector3> { gridLayout.CellToWorld(current) };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, gridLayout.CellToWorld(current));
        }
        return totalPath;
    }
}