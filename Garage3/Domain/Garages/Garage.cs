using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.General;
using Garage3.Domain.Interface;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Garages;

public class Garage<T> : IGarage<T>, IEnumerable<T> where T : Vehicle
{
  private readonly List<T> _vehicles = [];
  private readonly List<VehicleType> _types = [];
  public IEnumerable<T> Vehicles => _vehicles;
  public IEnumerable<VehicleType> Types => _types;
  public string Name { get; }
  public int ParkingCells { get; }
  public int TotalParkingLots =>
    ParkingCells / CellsPerParkingLot;
  public int UsedCells =>
  _vehicles.Sum(v => v.ParkingCells);
  public int AvailableCells =>
    ParkingCells - UsedCells;
  public int AvailableWholeParkingLots =>
    AvailableCells / CellsPerParkingLot;
  public int RemainingCells =>
      AvailableCells % CellsPerParkingLot;

  public const int MaxParkingLots = 200;
  public const int CellsPerParkingLot = 3;
  public const int MinNameLength = 3;

  public Garage(string name, int parkingLots, List<VehicleType> types)
  {
    if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength)
    {
      throw new ArgumentException("Name needs to have atleast 3 chars", nameof(name));
    }

    if (parkingLots <= 0 || parkingLots > MaxParkingLots)
    {
      throw new ArgumentException("Parkinglots needs to be more than zero and at the max 200", nameof(parkingLots));
    }

    if (types is null)
    {
      throw new ArgumentNullException("The garage needs one or more types", nameof(types));
    }

    if (types.Distinct().Count() != types.Count)
    {
      throw new ArgumentException("Must contain distinct types of vehicles", nameof(types));
    }

    Name = name;
    ParkingCells = parkingLots * CellsPerParkingLot;
    _vehicles = [];
    _types = types;
  }

  public bool AddVehicle(T vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    if (UsedCells + vehicle.ParkingCells > ParkingCells)
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