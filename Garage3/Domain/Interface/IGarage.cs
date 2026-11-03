using System.Collections.Generic;
using Garage3.Domain.Vehicles;

namespace Garage3.Domain.Interface;

public interface IGarage<T>
{
  public IEnumerable<T> Vehicles { get; }
  public string Name { get; }
  public int ParkingCells { get; }

  public bool AddVehicle(T vehicle);
  bool RemoveVehicle(string licenseplate);
}