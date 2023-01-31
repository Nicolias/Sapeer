using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.UI;

public class Test
{
    [Test]
    public void WhenCreatingNewMinefield_AndMinefieldCreating_ThenMinefieldLengthShouldBeMinefieldColumnMultiplyMinefieldRow()
    {
        // Arrange.
        Minefield minefield = new GameObject().AddComponent<Minefield>();

        // Act.
        minefield.CreateNewMinefieldCells(8, 8, 1, new GameObject().AddComponent<Button>());

        // Assert.
        minefield.Cells.Length.Should().Be(minefield.MinefieldColumn * minefield.MinefieldRow);
    }


    [Test]
    public void WhenMinefieldDisabling_AndMinefieldCellsCountIs64_ThenCellsCountIsZero()
    {
        // Arrange.
        Minefield minefield = new GameObject().AddComponent<Minefield>();

        // Act.
        minefield.CreateNewMinefieldCells(8, 8, 1, new GameObject().AddComponent<Button>());
        minefield.enabled = false;


        // Assert.
        foreach (Transform child in minefield.transform)
            child.GetComponent<Cell>().Should().NotBeNull();
    }
}
