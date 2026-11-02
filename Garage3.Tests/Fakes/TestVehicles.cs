using Garage3.Domain.Vehicles;

namespace Garage3.Tests.Fakes;

internal sealed class TestBoat(string licenseplate)
: Boat("RED", 0, licenseplate, 2);

internal sealed class TestCar(string licenseplate, int wheels = 4)
: Car("BLACK", wheels, licenseplate, 5, false);