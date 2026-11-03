

using System;
using Garage3.Domain.General;

namespace Garage3.Domain.Vehicles;

public class Car : Vehicle
{
  public int NumberOfDoors { get; }

  public bool IsElectric { get; }

  public override int ParkingCells => 3;

  public Car(string color, int numberOfWheels, string licensePlate, int numberOfDoors, bool isElectric)
      : base(VehicleType.Car, color, numberOfWheels, licensePlate)
  {
    if (numberOfDoors <= 0)
    {
      throw new ArgumentException("Number of doors must be greater than zero.", nameof(numberOfDoors));
    }

    NumberOfDoors = numberOfDoors;
    IsElectric = isElectric;
  }

  public override string GetInfo()
  {
    string boolString = IsElectric ? "Yes" : "No";
    string info = base.GetInfo();

    return info +
    $"\nAmount of doors: {NumberOfDoors}" +
    $"\nElectric: {boolString}";
  }
}