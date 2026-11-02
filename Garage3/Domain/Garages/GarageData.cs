using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Interface;

namespace Garage3.Domain.Garages;

public class GarageData
{
  private List<Garage> _garages = [];

  public IEnumerable<Garage> Garages => _garages;

  public void AddFacility(Garage facility)
  {
    ArgumentNullException.ThrowIfNull(facility);

    _garages.Add(facility);
  }

  public bool IsNameUnique(string name)
  {
    ArgumentNullException.ThrowIfNull(name);

    return !_garages.Any(g =>
    string.Equals(
      g.Name,
      name.Trim(),
      StringComparison.OrdinalIgnoreCase
    ));
  }

  public bool IsLicenseplateUnique(string license)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(license);

    return !_garages.SelectMany(g => g.Vehicles).Any(v => string.Equals(
      v.LicensePlate,
      license.Trim(),
      StringComparison.OrdinalIgnoreCase
    ));
  }

  public IEnumerable<IVehicle> GetAllVehicles()
  {
    return _garages.SelectMany(garage => garage.Vehicles);
  }

  public IEnumerable<Garage> GetAvailableGarages()
  {
    return _garages.Where(g => g.ParkingLots - g.Vehicles.Count() != 0);
  }
}

