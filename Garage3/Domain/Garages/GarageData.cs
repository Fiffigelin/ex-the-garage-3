using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Garages;

public class GarageData
{
  private List<Garage<Vehicle>> _garages = [];

  public IEnumerable<Garage<Vehicle>> Garages => _garages;

  public bool AddGarage(Garage<Vehicle> garage)
  {
    ArgumentNullException.ThrowIfNull(garage);

    if (IsNameTaken(garage.Name))
    {
      return false;
    }

    _garages.Add(garage);
    return true;
  }

  public bool IsNameTaken(string name)
  {
    ArgumentNullException.ThrowIfNull(name);

    return _garages.Any(g =>
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

  public IEnumerable<Garage<Vehicle>> GetAvailableGarages()
  {
    return _garages.Where(g => g.ParkingLots - g.Vehicles.Count() != 0);
  }
}

