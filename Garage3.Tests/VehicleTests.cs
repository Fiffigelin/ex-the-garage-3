using Garage3.Domain.Vehicles;

namespace Garage3.Tests;

public class VehicleTests
{
  [Fact]
  public void ShouldBeAble_ToCreate_VehicleTypeCar()
  {
    var vehicle = new Car("red", 4, "abc123", 5, false);

    Assert.IsType<Vehicle>(vehicle, exactMatch: false);
  }

  [Theory]
  [InlineData("ab456c")]
  [InlineData("1a2b3c")]
  [InlineData("123abc")]
  public void ShouldNotBeAble_ToCreate_CarWithInvalidLicenseplate(string licenseplate)
  {
    Assert.Throws<ArgumentException>(() => new Car("red", 4, licenseplate, 5, false));
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData("123abc")]
  [InlineData("1a2b3c")]
  public void InvalidInput_AsLicenseplate_ReturnsFalse(string licenseplate)
  {
    bool result = Vehicle.IsValidLicensePlate(licenseplate);

    Assert.False(result);
  }

  [Theory]
  [InlineData("abc123")]
  [InlineData("aBc123")]
  [InlineData("ABC123")]
  [InlineData("abC12d")]
  public void ValidInput_AsLicenseplate_ReturnsTrue(string licenseplate)
  {
    bool result = Vehicle.IsValidLicensePlate(licenseplate);

    Assert.True(result);
  }
}

// Vänta vad är det som händer? Har jag kul medan jag skriver tester?? 