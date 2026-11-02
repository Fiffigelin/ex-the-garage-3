
using System.IO;
using System.Text.Json;
using Garage3.Domain.Vehicles;
using Garage3.Domain.Garages;

namespace Garage3.Data;

public static class GarageSeeder
{
  // refaktor: ändra från svenska till engelska
  public static void Seed(GarageData garageData, string filePath)
  {
    string json = File.ReadAllText(filePath);

    JsonSerializerOptions options = new()
    {
      PropertyNameCaseInsensitive = true
    };

    GarageSeedData seedData =
        JsonSerializer.Deserialize<GarageSeedData>(json, options)
        ?? throw new InvalidDataException(
            "Could not read the file.");

    foreach (GarageSeedItem item in seedData.Garages)
    {
      Garage<Vehicle> garage = new(item.Name, item.ParkingLots);

      foreach (VehicleSeedItem vehicleItem in item.Vehicles)
      {
        Vehicle vehicle = VehicleSeedFactory.Create(vehicleItem);

        if (!garage.AddVehicle(vehicle))
        {
          throw new InvalidDataException(
              $"Could not park vehicle in Garage {item.Name}.");
        }
      }

      garageData.AddFacility(garage);
    }
  }
}
