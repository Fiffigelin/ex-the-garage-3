using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Vehicles;

namespace Garage3.Application;

public class GarageHandler : IHandler
{
  private readonly GarageData _data;

  public GarageHandler(GarageData data)
  {
    _data = data;
  }

  public bool IsGarageNameUnique(string name)
  {
    return !_data.IsNameTaken(name);
  }

  public bool AddGarage(string name, int parkinglots)
  {
    if (_data.IsNameTaken(name))
    {
      return false;
    }

    var garage = new Garage<Vehicle>(name, parkinglots);

    _data.AddFacility(garage);
    return true;
  }

  public IEnumerable<VehicleSearchResult> GetAllVehicles()
  {
    return _data.Garages.SelectMany(garage => garage.Vehicles, (garage, vehicle) => new VehicleSearchResult(garage, vehicle));
  }

  public IEnumerable<Garage<Vehicle>> GetGarages()
  {
    return _data.Garages;
  }

  public IEnumerable<Garage<Vehicle>> GetAvailableGarages()
  {
    return _data.GetAvailableGarages();
  }

  public bool ParkVehicle(string garageName, Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    if (!_data.IsLicenseplateUnique(vehicle.LicensePlate))
    {
      return false;
    }

    Garage<Vehicle>? garage = _data.Garages.FirstOrDefault(g =>
    string.Equals(
      g.Name,
      garageName,
      StringComparison.OrdinalIgnoreCase
    ));

    if (garage is null)
    {
      return false;
    }

    return garage.AddVehicle(vehicle);
  }

  public bool RemoveVehicle(string garageName, string licenseplate)
  {
    Garage<Vehicle>? garage = _data.Garages.FirstOrDefault(g =>
    string.Equals(
      g.Name,
      garageName,
      StringComparison.OrdinalIgnoreCase
    ));

    return garage?.RemoveVehicle(licenseplate) ?? false;
  }

  public IEnumerable<VehicleSearchResult> SearchVehicles(SearchVehicle filter)
  {
    return [.. _data.Garages
        .SelectMany(garage => garage.Vehicles
          .Where(filter.Matches)
          .Select(vehicle => new VehicleSearchResult(garage, vehicle)))];
  }
}