using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public int cellSize = -1;
    private Tile[] tiles;
    public Tile[] Tiles { get { return tiles; } }
    public int mapRow = -1;
    public int mapCol = -1;

    Dictionary<AdjacentDirection, CellManager> neighbours = new Dictionary<AdjacentDirection, CellManager>();

    public void ChangeTile() {/*�̵��Ұ��� �̵��Ҽ� �ְ� ó���ϴ� �ڵ带 �߰��� �����Դϴ�.*/}

    public Tile GetTile(int row, int col)
    {
        if (row < 0 || col < 0 || row >= cellSize || col >= cellSize)
        {
            // �ε����� ������ ��� ���
            if (neighbours.ContainsKey(AdjacentDirection.Up) && row < 0)
            {
                return neighbours[AdjacentDirection.Up].GetTile(cellSize + row, col);
            }
            else if (neighbours.ContainsKey(AdjacentDirection.Down) && row >= cellSize)
            {
                return neighbours[AdjacentDirection.Down].GetTile(row - cellSize, col);
            }
            else if (neighbours.ContainsKey(AdjacentDirection.Left) && col < 0)
            {
                return neighbours[AdjacentDirection.Left].GetTile(row, cellSize + col);
            }
            else if (neighbours.ContainsKey(AdjacentDirection.Right) && col >= cellSize)
            {
                return neighbours[AdjacentDirection.Right].GetTile(row, col - cellSize);
            }
            else
            {
                return null; // �ε����� ������ �������, ������ CellManager�� ���� ���
            }
        }
        return tiles[row * cellSize + col];
    }

    public void AddNeighbour(CellManager adjacentCellManager, AdjacentDirection direction)
    {
        neighbours[direction] = adjacentCellManager;
    }

    public void InitTileCells()
    {
        if (cellSize == -1)
            return;

        tiles = new Tile[cellSize * cellSize];

        for (int row = 0; row < cellSize; row++)
        {
            for (int col = 0; col < cellSize; col++)
            {
                Tile tile = UtillHelper.Find<Tile>(transform, "Row"+row.ToString()+"/"+"Col"+col.ToString());
                if(tile != null)
                {
                    bool isWalkable = true;
                    if (tile.gameObject.tag == "NotWalkable")
                        isWalkable = false;
                    tile.Init(row, col, isWalkable);
                    tiles[row * cellSize + col] = tile; // Ÿ���� tiles �迭�� �߰��մϴ�.
                }
            }
        }
    }

    public void Init(int mapRow, int mapCol)
    {
        this.mapRow = mapRow;
        this.mapCol = mapCol;
        InitTileCells();
    }
}
