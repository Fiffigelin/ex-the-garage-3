using System;
using System.Collections.Generic;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Garages;

public class GarageData
{
  private List<Garage> _garages = [];

  public IReadOnlyList<Garage> Garages => _garages;

  public void AddFacility(Garage facility)
  {
    ArgumentNullException.ThrowIfNull(facility);

    _garages.Add(facility);
  }

  public bool IsNameUnique(string name)
  {
    foreach (Garage facility in _garages)
    {
      if (string.Equals(
        facility.Name,
        name,
        StringComparison.OrdinalIgnoreCase
      ))
      {
        return false;
      }
    }

    return true;
  }

  public List<Garage> AvailableParking()
  {
    List<Garage> available = [];

    foreach (Garage garage in _garages)
    {
      if (garage.Vehicles.Count < garage.ParkingLots)
      {
        available.Add(garage);
      }
    }

    return available;
  }

  // bättre läsbarhet, ändrade pf till garage i foreachen
  public bool IsLicenseplateUnique(string license)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(license);

    foreach (Garage garage in _garages)
    {
      List<Vehicle> vehicles = [.. garage.Vehicles];

      foreach (Vehicle? vh in vehicles)
      {
        if (string.Equals(
            vh.LicensePlate,
            license.Trim(),
            StringComparison.OrdinalIgnoreCase))
        {
          return false;
        }
      }
    }

    return true;
  }
}

