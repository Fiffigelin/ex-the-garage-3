using System.Collections.Generic;
using Garage3.Domain.General;
using Garage3.Domain.Garages;
using Garage3.Domain.Vehicles;

public interface IHandler
{
  // Garage
  IEnumerable<Garage<Vehicle>> GetGarages();
  IEnumerable<Garage<Vehicle>> GetAvailableGarages(VehicleType type);
  bool IsGarageNameUnique(string name);
  bool AddGarage(string name, int parkinglots, List<VehicleType> types);

  //Vehicles
  bool ParkVehicle(string garageName, Vehicle vehicle);
  bool RemoveVehicle(string garageName, string licenseplate);

  // Search
  IEnumerable<VehicleSearchResult> GetAllVehicles();
  IEnumerable<VehicleSearchResult> SearchVehicles(SearchVehicle filter);
}