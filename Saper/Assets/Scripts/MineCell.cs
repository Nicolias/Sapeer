using UnityEngine;

public class MineCell : Cell
{
    protected override void OpenCell()
    {
        Debug.Log("Game Over");
        CellButton.image.color = Color.red;
    }
}