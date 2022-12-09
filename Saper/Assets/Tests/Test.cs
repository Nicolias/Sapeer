using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

public class Test
{

    [Test]
    public void WhenCreatingNewMinefield_AndMinefieldCreating_ThenMinefieldLengthShouldBeMinefieldColumnMultiplyMinefieldRow()
    {
        // Arrange.
        Minefield minefield = new GameObject().AddComponent<Minefield>();

        // Act.
        minefield.enabled = true;

        // Assert.
        minefield.Cells.Length.Should().Be(minefield.MinefieldColumn * minefield.MinefieldRow);        
    }
}
