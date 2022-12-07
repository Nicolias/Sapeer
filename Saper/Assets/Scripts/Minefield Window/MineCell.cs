using System;
using UnityEngine;

public class MineCell : Cell
{
    public event Action OnMineExploded;

    protected override void OpeningCell()
    {
        OnMineExploded?.Invoke();

        CellButton.image.color = Color.red;
    }
}