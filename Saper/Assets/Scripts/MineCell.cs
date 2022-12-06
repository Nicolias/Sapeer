using UnityEngine;

public class MineCell : Cell
{
    protected override void OpeningCell()
    {
        Debug.Log("Game Over");
        CellButton.image.color = Color.red;
    }
}