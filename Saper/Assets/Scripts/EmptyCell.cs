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

    protected override void OpenCell()
    {
        CellButton.interactable = false;
        CellButton.image.color = Color.green;

        FindClosestMine();
        _numberClosestMineText.text = _numberClosestMine.ToString();
    }

    private void FindClosestMine()
    {
        for (int i = Index.Item1 - 1; i <= Index.Item1 + 1; i++)
        {
            for (int j = Index.Item2 - 1; j <= Index.Item2 + 1; j++)
            {
                if (i < 0 || i > Minefield.MinefieldRow - 1 || j < 0 || j > Minefield.MinefieldColumn - 1)
                    continue;

                if (Minefield.Cells[i, j] is MineCell)
                    _numberClosestMine++;
            }
        }
    }
}
