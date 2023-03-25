using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row = -1;
    public int col = -1;
    public bool isWalkable = true;
    public int oppositeRow = -1;
    public int oppositeCol = -1;

    //Ÿ���� ���� �ʱ�ȭ���� �Լ��Դϴ�. 
    public void Init(int row, int col, bool isWalkable, int oppositeRow = -1, int oppositeCol = -1)
    {
        this.row = row;
        this.col = col;
        this.isWalkable = isWalkable;
        this.oppositeRow = oppositeRow;
        this.oppositeCol = oppositeCol;
    }
}
