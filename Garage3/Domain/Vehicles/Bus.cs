using System;
using Garage3.Domain.General;

namespace Garage3.Domain.Vehicles;

public class Bus : Vehicle
{
  public int NumberOfSeats { get; }
  public override int ParkingCells => 6;

  public Bus(string color, int numberOfWheels, string licensePlate, int numberOfSeats)
      : base(VehicleType.Bus, color, numberOfWheels, licensePlate)
  {
    if (numberOfSeats <= 0)
    {
      throw new ArgumentException("Number of seats must be greater than zero.", nameof(numberOfSeats));
    }

    NumberOfSeats = numberOfSeats;
  }

  public override string GetInfo()
  {
    string info = base.GetInfo();
    return info +
      $"\nAmount of seats: {NumberOfSeats}";
  }
}