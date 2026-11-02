using Garage3.Domain.Vehicles;

namespace Garage3.Application;

public class VehicleFactory
{
  public Car CreateCar(
    string licenseplae,
    string color,
    int numberOfWheels,
    int numberOfDoors,
    bool isElectric
  )
  {
    return new Car(
      color,
      numberOfWheels,
      licenseplae,
      numberOfDoors,
      isElectric
    );
  }

  public Motorcycle CreateMotorcycle(
    string licenseplate,
    string color,
    int numberOfWheels,
    bool hasSidecar
  )
  {
    return new Motorcycle(
      color, numberOfWheels, licenseplate, hasSidecar
    );
  }

  public Bus CreateBus(
    string licenseplate,
    string color,
    int numberOfWheels,
    int numberOfSeats
  )
  {
    return new Bus(
      color, numberOfWheels, licenseplate, numberOfSeats
    );
  }

  public Boat CreateBoat(
    string licenseplate,
    string color,
    int numberOfEngines

  )
  {
    return new Boat(
      color, 0, licenseplate, numberOfEngines
    );
  }

  public Airplane CreateAirplane(
    string licenseplate,
    string color,
    int numberOfWheels,
    int numberOfPassengers
  )
  {
    return new Airplane(
      color, numberOfWheels, licenseplate, numberOfPassengers
    );
  }
}