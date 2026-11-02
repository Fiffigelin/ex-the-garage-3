using System.Collections.Generic;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Interface;

public interface IGarage
{
  public IEnumerable<IVehicle> Vehicles { get; }
  public string Name { get; }
  public int ParkingLots { get; }

  public bool AddVehicle(IVehicle vehicle);
  bool RemoveVehicle(string licenseplate);
}