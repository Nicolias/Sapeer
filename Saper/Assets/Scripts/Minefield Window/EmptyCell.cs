using TMPro;
using UnityEngine;

public class EmptyCell : Cell
{
    private TMP_Text _numberClosestMineText;
    private int _numberClosestMine;

    public int NumberClosestMine => _numberClosestMine;

    private void Start()
    {
        _numberClosestMineText = GetComponentInChildren<TMP_Text>();   
    }

    public override void Reset()
    {
        base.Reset();

        _numberClosestMine = 0;
        _numberClosestMineText.text = "";
    }

    protected override void OpeningCell()
    {
        CellButton.image.color = Color.green;

        CalculateNumberClosestMine();

        if (_numberClosestMineText == null)
        {
            Debug.Log("Негде отображать число ближайших мин");
            return;
        }

        if (_numberClosestMine != 0)
            _numberClosestMineText.text = _numberClosestMine.ToString();
        else
            OpenClosestCells();
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

    private void OpenClosestCells()
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
