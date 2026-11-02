using Garage3.Domain.Garages;
using Garage3.Domain.Vehicles;
using Garage3.Tests.Fakes;

namespace Garage3.Tests;

public class GarageTests
{
    // [Theory] körs flera gånger om med olika indata [InlineData()]
    // Testar att skapa Garage som inte uppfyller kraven
    // Första testar namn och andra testet testar parkeringsplatser

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ab")]
    public void Constructor_InvalidName_ThrowsArgumentException(string name)
    {
        Assert.Throws<ArgumentException>(() => new Garage<TestBoat>(name, 10));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(201)]
    public void Constructor_InvalidParkinglosts_ThrowsArgumentException(int parkinglots)
    {
        Assert.Throws<ArgumentException>(() => new Garage<TestCar>("Test", parkinglots));
    }

    // skapar ett garage
    [Fact]
    public void CreateGarage_WithMinimumName_And_MaxParkingLots_DoesNotThrow()
    {
        // Arrange and Act
        var garage = new Garage<Vehicle>("Tre", Garage<Vehicle>.MaxParkingLots);

        // Assert
        Assert.Equal(200, garage.ParkingLots);
    }
}
