using System.ComponentModel;
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

    // add vehicle
    [Fact]
    public void AddVehicle_ValidVehicle_ReturnsTrue_And_ParksVehicle()
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 2);
        var vehicle = new TestCar("abc123");

        // Act
        bool result = garage.AddVehicle(vehicle);

        // Assert
        Assert.True(result);
        Assert.Single(garage);
    }

    [Fact]
    public void AddVehicle_WhenGarageIsFull_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 2);
        garage.AddVehicle(new TestCar("abc123"));
        garage.AddVehicle(new TestCar("abc12d"));

        Boat boat = new TestBoat("bcd123");

        // Act
        bool result = garage.AddVehicle(boat);

        // Assert
        Assert.False(result);
        Assert.DoesNotContain(boat, garage.Vehicles); // <== denna var svår att förstå från dokumentationen
        // behövde se ett exempel som jag kunda ta till mig: https://hamidmosalla.com/2020/01/12/xunit-part-2-value-and-type-based-assertions-in-xunit/
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    public void AddVehicle_SameLicenseplate_ReturnsFalse(string licenseplate)
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 5);
        garage.AddVehicle(new TestCar("ABC123"));

        // Act
        bool result = garage.AddVehicle(new TestBoat(licenseplate));

        // Assert
        Assert.False(result);
        Assert.Single(garage);
    }

    [Fact]
    public void AddVehicle_NullVehicle_ThrowsArgumentNullException()
    {
        // Arrange & Act
        var garage = new Garage<Vehicle>("Test", 1);

        // Assert
        Assert.Throws<ArgumentNullException>(() => garage.AddVehicle(null!));
    }
}
