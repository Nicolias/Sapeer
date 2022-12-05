using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Minefield : MonoBehaviour
{
    [SerializeField] int _minefieldRow, _minefieldColumn;
    public int MinefieldRow => _minefieldRow;
    public int MinefieldColumn => _minefieldColumn;

    [SerializeField] private int _mineCount;
    private Button[] _cellsTemplate;

    private Cell[,] _cells;
    public Cell[,] Cells => _cells;


    private void Awake()
    {
        _cellsTemplate = transform.GetComponentsInChildren<Button>();
        ShuffleCells();
        SetCellsComponent(_cellsTemplate, _mineCount);
        _cells = GetMinefieldGrid();
    }

    private void ShuffleCells()
    {
        System.Random random = new();

        for (int i = _cellsTemplate.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            
            var temp = _cellsTemplate[j];
            _cellsTemplate[j] = _cellsTemplate[i];
            _cellsTemplate[i] = temp;
        }
    }

    private void SetCellsComponent(Button[] cellsTemplate, int mineCount)
    {
        if (mineCount > cellsTemplate.Length) throw new InvalidOperationException
                ("Колличество бомб не может быть больше колличества ячеек");

        int c = 0;
        for (int i = 0; i < _minefieldRow; i++)
        {
            for (int j = 0; j < _minefieldColumn; j++)
            {
                if (c < mineCount)
                    cellsTemplate[c].gameObject.AddComponent<MineCell>().Minefield = this;
                else
                    cellsTemplate[c].gameObject.AddComponent<EmptyCell>().Minefield = this;

                c++;
            }
        }
    }   

    private Cell[,] GetMinefieldGrid()
    {
        Cell[,] minefieldGrid = new Cell[_minefieldRow, _minefieldColumn];

        int c = 0;
        for (int i = 0; i < _minefieldRow; i++)
        {
            for (int j = 0; j < _minefieldColumn; j++)
            {
                minefieldGrid[i, j] = transform.GetChild(c).GetComponent<Cell>();
                minefieldGrid[i, j].SetCellIndex((i, j));

                c++;
            }
        }

        return minefieldGrid;
    }
}
