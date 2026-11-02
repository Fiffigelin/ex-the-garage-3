using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Interface;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Garages;

public class Garage<T> : IGarage<T>, IEnumerable<T> where T : Vehicle
{
  private readonly List<T> _vehicles = [];
  public IEnumerable<T> Vehicles => _vehicles;
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

  public bool AddVehicle(T vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    if (_vehicles.Count == ParkingLots)
    {
      return false;
    }

    if (_vehicles.Any(v =>
      string.Equals(
        v.LicensePlate,
        vehicle.LicensePlate,
        StringComparison.OrdinalIgnoreCase
      )
    ))
    {
      return false;
    }

    _vehicles.Add(vehicle);
    return true;
  }

  public bool RemoveVehicle(string licenseplate)
  {
    ArgumentNullException.ThrowIfNull(licenseplate);

    T? vehicle = _vehicles.FirstOrDefault(v =>
    string.Equals(
      v.LicensePlate,
      licenseplate.Trim(),
      StringComparison.OrdinalIgnoreCase
    ));

    if (vehicle is null)
    {
      return false;
    }

    return _vehicles.Remove(vehicle);
  }

  public IEnumerator<T> GetEnumerator() => _vehicles.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}