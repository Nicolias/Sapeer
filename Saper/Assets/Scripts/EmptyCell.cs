using TMPro;
using UnityEngine;

public class EmptyCell : Cell
{
    private TMP_Text _numberClosestMineText;
    private int _numberClosestMine;

    private void Start()
    {
        _numberClosestMineText = GetComponentInChildren<TMP_Text>();   
    }

    protected override void OpeningCell()
    {
        CellButton.image.color = Color.green;

        CalculateNumberClosestMine();

        if (_numberClosestMine == 0)
            OpenClosesCells();
        else
            _numberClosestMineText.text = _numberClosestMine.ToString();
    }

    private void CalculateNumberClosestMine()
    {
        for (int i = CellIndex.Item1 - 1; i <= CellIndex.Item1 + 1; i++)
        {
            for (int j = CellIndex.Item2 - 1; j <= CellIndex.Item2 + 1; j++)
            {
                if (i < 0 || i > Minefield.MinefieldRow - 1 || j < 0 || j > Minefield.MinefieldColumn - 1)
                    continue;

                if (Minefield.Cells[i, j] is MineCell)
                    _numberClosestMine++;
            }
        }
    }

    private void OpenClosesCells()
    {
        for (int i = CellIndex.Item1 - 1; i <= CellIndex.Item1 + 1; i++)
        {
            for (int j = CellIndex.Item2 - 1; j <= CellIndex.Item2 + 1; j++)
            {
                if (i < 0 || i > Minefield.MinefieldRow - 1 || j < 0 || j > Minefield.MinefieldColumn - 1 
                    || (i == CellIndex.Item1 && j == CellIndex.Item2))
                    continue;

                if (Minefield.Cells[i, j].IsOpened)
                    continue;

                Minefield.Cells[i, j].OpenCell();
            }
        }
    }
}
