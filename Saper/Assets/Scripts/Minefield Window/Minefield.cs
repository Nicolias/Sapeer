using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minefield : MonoBehaviour
{
    public event Action OnGameOver;

    [SerializeField] private AudioSource _explosionSound;

    [SerializeField] private int _minefieldRow, _minefieldColumn, _mineCount;

    [SerializeField] private Button _cellTemplate;

    private List<MineCell> _mineCellsCollection = new();

    public int MinefieldRow => _minefieldRow;
    public int MinefieldColumn => _minefieldColumn;

    private Cell[,] _cells;
    public Cell[,] Cells => _cells;

    private void OnEnable()
    {
        CreateNewMinefield();
    }

    private void OnDisable()
    {
        foreach (var mineCell in _mineCellsCollection)
            mineCell.OnMineExploded -= StartGameOver;

        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void ResetMinefield()
    {
        foreach (var cell in Cells)
            cell.Reset();

        ShuffleCells(transform.GetComponentsInChildren<Button>());
        _cells = GetMinefieldGrid();
    }

    private void CreateNewMinefield()
    {
        List<Button> newCells = new();

        for (int i = 0; i < _minefieldRow; i++)
            for (int j = 0; j < _minefieldColumn; j++)
                newCells.Add(Instantiate(_cellTemplate, transform));

        SetCellsComponent(newCells.ToArray(), _mineCount);
        ShuffleCells(newCells.ToArray());
        _cells = GetMinefieldGrid();
    }

    private void ShuffleCells(Button[] newCells)
    {
        System.Random random = new();

        for (int i = newCells.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);            
            
            var temp = newCells[j].transform.GetSiblingIndex();
            newCells[j].transform.SetSiblingIndex(i);
            newCells[i].transform.SetSiblingIndex(temp);
        }
    }

    private void SetCellsComponent(Button[] newCells, int mineCount)
    {
        if (mineCount > newCells.Length) throw new InvalidOperationException
                ("Колличество бомб не может быть больше колличества ячеек");

        int c = 0;
        for (int i = 0; i < _minefieldRow; i++)
        {
            for (int j = 0; j < _minefieldColumn; j++)
            {
                if (c < mineCount)
                {
                    MineCell newCell = newCells[c].gameObject.AddComponent<MineCell>();
                    _mineCellsCollection.Add(newCell);
                    newCell.Minefield = this;
                    newCell.OnMineExploded += StartGameOver;
                }
                else
                {
                    newCells[c].gameObject.AddComponent<EmptyCell>().Minefield = this;
                }

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

    private void StartGameOver()
    {
        OnGameOver?.Invoke();

        _explosionSound.Play();

        foreach (Cell cell in Cells)
            cell.CellButton.interactable = false;
    }
}
