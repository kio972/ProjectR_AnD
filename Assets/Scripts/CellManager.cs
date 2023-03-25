using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public int cellSize = -1;
    private Tile[,] tiles;

    // �ش� �ڵ尡 ȣ��� �Լ��� �ۼ��Ͽ����մϴ�. �� �Լ��� MapGenerator���� ȣ��� �����Դϴ�.
    public void InitTileCells()
    {
        if (cellSize == -1)
            return;

        tiles = new Tile[cellSize, cellSize];

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
                }    
            }
        }
    }

}
