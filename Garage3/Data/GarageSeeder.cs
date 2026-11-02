
using System.IO;
using System.Text.Json;
using Garage3.Domain.Garages;

namespace Garage3.Data;

// Denna klass läser seed datan
public static class GarageSeeder
{
  public static GarageData Seed(string filePath)
  {
    string json = File.ReadAllText(filePath);

    GarageStorageData seedData =
        JsonSerializer.Deserialize<GarageStorageData>(
            json,
            new JsonSerializerOptions
            {
              PropertyNameCaseInsensitive = true
            })
        ?? throw new InvalidDataException("Could not read the file.");

    return GarageStorageMapper.ToGarageData(seedData);
  }
}
