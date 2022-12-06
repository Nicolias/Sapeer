using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minefield : MonoBehaviour
{
    [SerializeField] private Button _resetButton, _buttonTemplate;
    [SerializeField] private int _minefieldRow, _minefieldColumn;

    public int MinefieldRow => _minefieldRow;
    public int MinefieldColumn => _minefieldColumn;

    [SerializeField] private int _mineCount;
    private Button[] _cellsTemplate;

    private Cell[,] _cells;
    public Cell[,] Cells => _cells;


    private void Awake()
    {
        CreateNewMinefield();
    }

    private void OnEnable()
    {
        _resetButton.onClick.AddListener(ResetMinefield);
    }

    private void OnDisable()
    {
        _resetButton.onClick.AddListener(ResetMinefield);
    }

    private void ResetMinefield()
    {
        foreach (var cell in Cells)
            cell.Reset();

        ShuffleCells();
        _cells = GetMinefieldGrid();
    }

    private void CreateNewMinefield()
    {
        List<Button> newCells = new();

        for (int i = 0; i < _minefieldRow; i++)
        {
            for (int j = 0; j < _minefieldColumn; j++)
            {
                newCells.Add(Instantiate(_buttonTemplate, transform));
            }
        }

        _cellsTemplate = newCells.ToArray();

        SetCellsComponent(_cellsTemplate, _mineCount);
        ShuffleCells();
        _cells = GetMinefieldGrid();
    }

    private void ShuffleCells()
    {
        System.Random random = new();

        for (int i = _cellsTemplate.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);            
            
            var temp = _cellsTemplate[j].transform.GetSiblingIndex();
            _cellsTemplate[j].transform.SetSiblingIndex(i);
            _cellsTemplate[i].transform.SetSiblingIndex(temp);
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
                minefieldGrid[i, j].CellIndex = (i, j);

                c++;
            }
        }

        return minefieldGrid;
    }
}
