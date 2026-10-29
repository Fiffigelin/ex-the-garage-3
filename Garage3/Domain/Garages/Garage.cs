using System;
using System.Collections.Generic;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Garages;

public class Garage
{
  private readonly List<Vehicle> _vehicles = [];
  public IReadOnlyList<Vehicle> Vehicles => _vehicles;
  public string Name { get; }
  public int ParkingLots { get; }
  public const int MaxParkingLots = 200;
  public const int MinNameLength = 3;

  public Garage(string name, int parkingLots)
  {
    if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength)
    {
      throw new ArgumentException("Name needs to have atleast 3 chars", nameof(name));
    }

    if (parkingLots <= 0 || parkingLots > MaxParkingLots)
    {
      throw new ArgumentException("Parkinglots needs to be more than zero and at the max 200", nameof(parkingLots));
    }

    Name = name;
    ParkingLots = parkingLots;
    _vehicles = [];
  }

  public bool AddVehicle(Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);
    foreach (Vehicle vh in _vehicles)
    {
      if (vh.LicensePlate == vehicle.LicensePlate)
      {
        return false;
      }
    }

    _vehicles.Add(vehicle);
    return true;
  }

  public bool RemoveVehicle(Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    foreach (Vehicle vh in _vehicles)
    {
      if (vh.LicensePlate == vehicle.LicensePlate)
      {
        _vehicles.Remove(vh);
        return true;
      }
    }

    return false;
  }

  public int CountParkedVehicles()
  {
    return _vehicles.Count;
  }
}