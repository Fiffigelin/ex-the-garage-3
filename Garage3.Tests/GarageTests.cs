using Garage3.Domain.Garages;
using Garage3.Domain.General;
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
        Assert.Throws<ArgumentException>(() => new Garage<TestBoat>(name, 10, [VehicleType.Car]));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(201)]
    public void Constructor_InvalidParkinglosts_ThrowsArgumentException(int parkinglots)
    {
        Assert.Throws<ArgumentException>(() => new Garage<TestCar>("Test", parkinglots, [VehicleType.Car]));
    }

    [Theory]
    [InlineData(new VehicleType[] { VehicleType.Car })]
    [InlineData(new VehicleType[]
{
    VehicleType.Car,
    VehicleType.Bus,
    VehicleType.Airplane,
    VehicleType.Motorcycle,
    VehicleType.Boat
    })]
    public void Constructor_AcceptsOneorMoreVehicleTypes_WithoutThrowException(VehicleType[] types)
    {
        var garage = new Garage<Vehicle>("Tre", 20, [.. types]);

        Assert.Equal(types.Length, garage.Types.Count());

        Assert.All(types, type =>
            Assert.Contains(type, garage.Types));
    }

    // skapar ett garage
    [Fact]
    public void CreateGarage_WithMinimumName_And_MaxParkingLots_DoesNotThrow()
    {
        // Arrange and Act
        var garage = new Garage<Vehicle>("Tre", Garage<Vehicle>.MaxParkingLots, [VehicleType.Car]);

        // Assert
        Assert.Equal(200, garage.TotalParkingLots);
    }

    // add vehicle
    [Fact]
    public void AddVehicle_ValidVehicle_ReturnsTrue_And_ParksVehicle()
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 2, [VehicleType.Car]);
        var vehicle = new TestCar("abc123");

        // Act
        bool result = garage.AddVehicle(vehicle);

        // Assert
        Assert.True(result);
        Assert.Single(garage.Vehicles);
    }

    [Fact]
    public void AddVehicle_WhenGarageIsFull_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 2, [VehicleType.Car]);
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

    [Fact]
    public void Vehicles_TakeDifferent_AmountsOfParkingSpace()
    {
        var motorcycle = new Motorcycle("red", 2, "abc123", false);
        var car = new Car("red", 4, "bcd123", 3, false);
        var bus = new Bus("red", 6, "cde123", 52);
        var boat = new Boat("red", 0, "def123", 4);
        var airplane = new Airplane("red", 3, "efg123", 50);

        Assert.Equal(1, motorcycle.ParkingCells);
        Assert.Equal(3, car.ParkingCells);
        Assert.Equal(6, bus.ParkingCells);
        Assert.Equal(9, boat.ParkingCells);
        Assert.Equal(15, airplane.ParkingCells);
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    public void AddVehicle_SameLicenseplate_ReturnsFalse(string licenseplate)
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 5, [VehicleType.Car]);
        garage.AddVehicle(new TestCar("ABC123"));

        // Act
        bool result = garage.AddVehicle(new TestBoat(licenseplate));

        // Assert
        Assert.False(result);
        Assert.Single(garage.Vehicles);
    }

    [Fact]
    public void AddVehicle_NullVehicle_ThrowsArgumentNullException()
    {
        // Arrange & Act
        var garage = new Garage<Vehicle>("Test", 1, [VehicleType.Car]);

        // Assert
        Assert.Throws<ArgumentNullException>(() => garage.AddVehicle(null!));
    }

    // remove vehicles

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    public void RemoveVehicle_ExistingPlate_ReturnsTrue(string licenseplate)
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 5, [VehicleType.Car]);
        garage.AddVehicle(new TestCar("ABC123"));

        // Act
        bool result = garage.RemoveVehicle(licenseplate);

        // Assert
        Assert.True(result);
        Assert.Empty(garage.Vehicles);
    }

    [Fact]
    public void RemoveVehicle_WrongLicenseplate_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Vehicle>("Test", 5, [VehicleType.Car]);
        garage.AddVehicle(new TestCar("ABC123"));

        // Act
        bool result = garage.RemoveVehicle("ZZZ123");

        //Assert
        Assert.False(result);
        Assert.Single(garage.Vehicles);
    }

    [Fact]
    public void RemoveVehicle_NullLicenseplate_ThrowsArgumentNullException()
    {
        // Arrange & Act
        var garage = new Garage<Vehicle>("Test", 5, [VehicleType.Car]);

        //Assert
        Assert.Throws<ArgumentNullException>(() => garage.RemoveVehicle(null!));
    }

    [Fact]
    public void GetAvailableGarages_ShouldAccountForVehicleSize()
    {
        // Arrange
        var data = new GarageData();
        var smallGarage = new Garage<Vehicle>("Small", 2, [VehicleType.Airplane]);
        var largeGarage = new Garage<Vehicle>("Large", 10, [VehicleType.Airplane]);

        data.AddGarage(smallGarage);
        data.AddGarage(largeGarage);

        var airplane = new Airplane("Red", 3, "fly123", 52);

        // Act
        List<Garage<Vehicle>> garageOptions = [.. data.GetAvailableGarages(airplane.ParkingCells)];

        // Assert
        Assert.Single(garageOptions);
        Assert.Contains(garageOptions, g => g.Name == "Large");
        Assert.DoesNotContain(garageOptions, g => g.Name == "Small");
    }
}
