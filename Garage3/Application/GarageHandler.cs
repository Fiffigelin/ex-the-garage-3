using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Interface;

namespace Garage3.Application;

public class GarageHandler : IHandler
{
  private readonly GarageData _data;

  // ska jag byta till primary constructor?
  public GarageHandler(GarageData data)
  {
    _data = data;
  }

  public bool IsGarageNameUnique(string name)
  {
    return !_data.IsNameUnique(name);
  }

  public bool AddGarage(string name, int parkinglots)
  {
    if (!_data.IsNameUnique(name))
    {
      return false;
    }

    var garage = new Garage(name, parkinglots);

    _data.AddFacility(garage);
    return true;
  }

  public IEnumerable<VehicleSearchResult> GetAllVehicles()
  {
    throw new System.NotImplementedException();
  }

  // public IEnumerable<Garage> GetAvailableGarages(IVehicle vehicle)
  // {
  //   return _data.Garages;
  // }

  public IEnumerable<Garage> GetGarages()
  {
    return _data.Garages;
  }

  public IEnumerable<Garage> GetGaragesWithParking()
  {
    return _data.GetAvailableGarages();
  }

  public bool ParkVehicle(string garageName, IVehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    if (!_data.IsLicenseplateUnique(vehicle.LicensePlate))
    {
      return false;
    }

    Garage? garage = _data.Garages.FirstOrDefault(g =>
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
    Garage? garage = _data.Garages.FirstOrDefault(g =>
    string.Equals(
      g.Name,
      garageName,
      StringComparison.OrdinalIgnoreCase
    ));

    return garage?.RemoveVehicle(licenseplate) ?? false;
  }

  public IEnumerable<VehicleSearchResult> SearchVehicles(VehicleSearchResult filter)
  {
    throw new System.NotImplementedException();
  }
}