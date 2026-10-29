using System;
using Garage3.Domain.General;

namespace Garage3.Domain.Vehicles;

public class Airplane : Vehicle
{
  public int NumberOfPassengers { get; }

  public Airplane(string color, int numberOfWheels, string licensePlate, int numberOfPassengers)
      : base(VehicleType.Airplane, color, numberOfWheels, licensePlate)
  {
    if (numberOfPassengers <= 0)
    {
      throw new ArgumentException("Number of passengers must be greater than zero.", nameof(numberOfPassengers));
    }

    NumberOfPassengers = numberOfPassengers;
  }

  public override string GetInfo()
  {
    string info = base.GetInfo();

    return info +
    $"\nNumber of Passengers: {NumberOfPassengers}";
  }
}