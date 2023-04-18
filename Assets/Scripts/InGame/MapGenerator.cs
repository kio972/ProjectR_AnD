using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> cellPrefabs;
    [SerializeField] private int minCells = 1;
    [SerializeField] private int maxCells = 4;
    private int mapSize;
    List<CellManager> mapCells = new List<CellManager>();

    public void GenerateMap()
    {
        int numCells = GetCellNumber();
        print(numCells);
        mapSize = Mathf.CeilToInt(Mathf.Sqrt(numCells)) * 2;
        int[,] map = new int[mapSize, mapSize];
        int x = Random.Range(0, mapSize);
        int y = Random.Range(0, mapSize);
        InstantiateCell(x, y);
        map[x, y] = 1;
        int cellCount = 1;

        while (cellCount < numCells)
        {
            while(true)
            {
                int randX = Random.Range(0, mapSize);
                int randY = Random.Range(0, mapSize);
                if (map[randX, randY] == 0 && IsAdjacent(map, randX, randY))
                {
                    InstantiateCell(randX, randY);
                    map[randX, randY] = 1;
                    cellCount++;
                    break;
                }
            }
            
        }

        //InitMap();
        //PlayerController player = FindObjectOfType<PlayerController>();
        //player.ForcedSetPosition(mapCells[0].transform.position);
    }

    private void InitMap()
    {
        for (int i = 0; i < mapCells.Count; i++)
        {
            CellManager currentCellManager = mapCells[i];

            for (int j = i + 1; j < mapCells.Count; j++)
            {
                CellManager adjacentCellManager = mapCells[j];

                // 인접 방향 계산
                AdjacentDirection direction = UtillHelper.GetAdjacentDirection(currentCellManager, adjacentCellManager);

                // 인접 방향이 있다면 서로의 이웃 리스트에 추가
                if (direction != AdjacentDirection.None)
                {
                    currentCellManager.AddNeighbour(adjacentCellManager, direction);
                    adjacentCellManager.AddNeighbour(currentCellManager, UtillHelper.GetOppositeDirection(direction));
                }
            }
        }

        foreach(CellManager cellManager in mapCells)
        {
            foreach (Tile tile in cellManager.Tiles)
                tile.CalculateAdjacentTiles();
        }
    }

    private void InstantiateCell(int x, int y)
    {
        int cellIndex = GetCellIndex();
        GameObject cellObject = cellPrefabs[cellIndex];
        Vector3 cellPos = GetCellPos(x, y);
        //GameObject cellObject = Instantiate(cellPrefab, cellPos, Quaternion.identity);
        cellObject.transform.SetParent(transform);
        cellObject.transform.position = cellPos;
        CellManager cellManager = cellObject.GetComponent<CellManager>();
        cellManager.Init(x, y);
        mapCells.Add(cellManager);
        cellPrefabs.Remove(cellObject);
    }

    private int GetCellNumber()
    {
        return Random.Range(minCells, maxCells + 1);
    }

    // 재정의 필요
    private Vector3 GetCellPos(int x, int y)
    {
        float posX = x * 36;
        float posY = 0;
        float posZ = y * 36;
        return new Vector3(posX, posY, posZ);
    }

    private int GetCellIndex()
    {
        return Random.Range(0, cellPrefabs.Count);
    }

    private bool IsAdjacent(int[,] map, int x, int y)
    {
        int[] dx = { 0, 0, -1, 1 }; // x축 이동 방향
        int[] dy = { -1, 1, 0, 0 }; // y축 이동 방향

        for (int i = 0; i < dx.Length; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < map.GetLength(0) && ny >= 0 && ny < map.GetLength(1))
            {
                if (map[nx, ny] != 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //private bool IsAdjacent(int[,] map, int x, int y)
    //{
    //    for (int i = x - 1; i <= x + 1; i++)
    //    {
    //        for (int j = y - 1; j <= y + 1; j++)
    //        {
    //            if (i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1))
    //            {
    //                if (map[i, j] != 0)
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //    }

    //    return false;
    //}


    // 셀을 생성한 뒤 셀간 이동할수 있는지 확인하는 함수입니다. 이후 CellManager의 ChangeTile()에서 사용할 예정입니다.
    public bool IsCellPassable(CellManager cellManager1, CellManager cellManager2)
    {
        // cellManager1에 속한 Tile들의 adjacentTile이 cellManager2에 속한 Tile 중에 있는지 확인합니다.
        foreach (Tile tile1 in cellManager1.Tiles)
        {
            foreach (Tile tile2 in cellManager2.Tiles)
            {
                foreach (Tile adjaccentTile in tile1.adjacentTiles)
                {
                    if (adjaccentTile == tile2)
                    {
                        return true;
                    }
                }
            }
        }

        // 인접하지 않은 경우 false를 반환합니다.
        return false;
    }
}
