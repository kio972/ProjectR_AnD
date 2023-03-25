using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row = -1;
    public int col = -1;
    public bool isWalkable = true;
    public List<Tile> adjacentTiles = new List<Tile>();

    //Ÿ���� ���� �ʱ�ȭ���� �Լ��Դϴ�.
    public void Init(int row, int col, bool isWalkable)
    {
        this.row = row;
        this.col = col;
        this.isWalkable = isWalkable;
    }

    public void CalculateAdjacentTiles()
    {
        adjacentTiles.Clear();
        CellManager cellManager = GetComponentInParent<CellManager>();
        // ���� Ÿ���� ��, �Ʒ�, ����, ������ Ÿ���� �����ɴϴ�.
        Tile upTile = cellManager.GetTile(row - 1, col);
        Tile downTile = cellManager.GetTile(row + 1, col);
        Tile leftTile = cellManager.GetTile(row, col - 1);
        Tile rightTile = cellManager.GetTile(row, col + 1);

        // �� Ÿ���� null�� �ƴϸ� adjacentTiles ����Ʈ�� �߰��մϴ�.
        if (upTile != null)
            adjacentTiles.Add(upTile);
        if (downTile != null)
            adjacentTiles.Add(downTile);
        if (leftTile != null)
            adjacentTiles.Add(leftTile);
        if (rightTile != null)
            adjacentTiles.Add(rightTile);
    }
}
