using System.IO;
using Garage3.Domain.Garages;
using Garage3.Domain.Vehicles;

namespace Garage3.Data;

public static class GarageStorageMapper
{
  // Mappar Jsonobject till Domänobjekt
  public static GarageData ToGarageData(GarageStorageData data)
  {
    GarageData garageData = new();

    foreach (GarageStorageItem item in data.Garages)
    {
      Garage<Vehicle> garage = new(item.Name, item.ParkingCells / 3, item.Types);

      foreach (VehicleStorageItem vehicleItem in item.Vehicles)
      {
        Vehicle vehicle = VehicleStorageMapper.ToVehicle(vehicleItem);

        if (!garage.AddVehicle(vehicle))
        {
          throw new InvalidDataException(
              $"Could not park vehicle in Garage {item.Name}.");
        }
      }

      garageData.AddGarage(garage);
    }

    return garageData;
  }

  // Mappar Domänobjekt till Jsonobjekt
  public static GarageStorageData ToStorageData(GarageData data)
  {
    GarageStorageData storageData = new();

    foreach (Garage<Vehicle> garage in data.Garages)
    {
      GarageStorageItem garageItem = new()
      {
        Name = garage.Name,
        ParkingCells = garage.ParkingCells,
        Types = [.. garage.Types]
      };

      foreach (Vehicle vehicle in garage.Vehicles)
      {
        VehicleStorageItem item = VehicleStorageMapper.ToStorageItem(vehicle);

        garageItem.Vehicles.Add(item);
      }

      storageData.Garages.Add(garageItem);
    }

    return storageData;
  }
}