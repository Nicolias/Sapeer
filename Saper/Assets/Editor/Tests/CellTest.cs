using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.UI;
using TMPro;

public class CellTest
{
    [Test]
    public void WhenOpeningEmptyCell_AndAroundThreeMine_ThenNamberClosestMineEquelsThree()
    {
        // Arrange.
        EmptyCell emptyCell = new GameObject().AddComponent<Button>().gameObject.AddComponent<EmptyCell>();
        var textObject = new GameObject();
        textObject.transform.SetParent(emptyCell.transform);
        textObject.AddComponent<TMP_Text>();
        emptyCell.CellIndex = (1,1);

        Minefield minefield = new GameObject().AddComponent<Minefield>();
        Cell[,] cells = new Cell[3, 3] { 
            { new GameObject().AddComponent<MineCell>(), new GameObject().AddComponent<MineCell>(), new GameObject().AddComponent<EmptyCell>() }, 
            { new GameObject().AddComponent<EmptyCell>(), new GameObject().AddComponent<EmptyCell>(), new GameObject().AddComponent<EmptyCell>() },
            { new GameObject().AddComponent<EmptyCell>(), new GameObject().AddComponent<EmptyCell>(), new GameObject().AddComponent<MineCell>()}};

        minefield.PastCellsGridTemplate(cells);
        emptyCell.GetComponent<Button>().image = emptyCell.gameObject.AddComponent<Image>();
        emptyCell.Minefield = minefield;

        // Act.
        emptyCell.OpenCell();

        // Assert.
        emptyCell.NumberClosestMine.Should().Be(3);
    } 
}
