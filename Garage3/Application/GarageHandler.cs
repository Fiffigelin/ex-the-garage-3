using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Data;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Vehicles;

namespace Garage3.Application;

public class GarageHandler : IHandler
{
  private readonly GarageData _data;
  private readonly GarageRepository _repository;

  public GarageHandler(GarageData data, GarageRepository repository)
  {
    _data = data;
    _repository = repository;
  }

  public bool IsGarageNameUnique(string name)
  {
    return !_data.IsNameTaken(name);
  }

  public bool AddGarage(string name, int parkinglots)
  {
    var garage = new Garage<Vehicle>(name, parkinglots);

    bool saved = _data.AddGarage(garage);

    if (!saved)
    {
      return false;
    }

    _repository.Save(_data);
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

    bool parked = garage.AddVehicle(vehicle);
    if (!parked)
    {
      return false;
    }

    _repository.Save(_data);
    return true;
  }

  public bool RemoveVehicle(string garageName, string licenseplate)
  {
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

    bool removed = garage.RemoveVehicle(licenseplate);
    if (!removed)
    {
      return false;
    }

    _repository.Save(_data);
    return true;
  }

  public IEnumerable<VehicleSearchResult> SearchVehicles(SearchVehicle filter)
  {
    return [.. _data.Garages
        .SelectMany(garage => garage.Vehicles
          .Where(filter.Matches)
          .Select(vehicle => new VehicleSearchResult(garage, vehicle)))];
  }
}