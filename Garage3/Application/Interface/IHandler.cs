using System.Collections.Generic;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Interface;

public interface IHandler
{
  // Garage
  IEnumerable<Garage> GetGarages();
  IEnumerable<Garage> GetGaragesWithParking();
  bool IsGarageNameUnique(string name);
  bool AddGarage(string name, int parkinglots);

  //Vehicles
  bool ParkVehicle(string garageName, IVehicle vehicle);
  bool RemoveVehicle(string garageName, string licenseplate);

  // Search
  IEnumerable<VehicleSearchResult> GetAllVehicles();
  IEnumerable<VehicleSearchResult> SearchVehicles(VehicleSearchResult filter);

  //Abailability
  // IEnumerable<Garage> GetAvailableGarages(IVehicle vehicle);
}