
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Garage3.Domain.Garages;

namespace Garage3.Data;

// Denna klass läser seed datan
public static class GarageSeeder
{
  private static readonly JsonSerializerOptions _options = new()
  {
    PropertyNameCaseInsensitive = true,
    Converters =
    {
      new JsonStringEnumConverter()
    }
  };

  public static GarageData Seed(string filePath)
  {
    string json = File.ReadAllText(filePath);

    GarageStorageData seedData =
        JsonSerializer.Deserialize<GarageStorageData>(
          json,
          _options
        )
        ?? throw new InvalidDataException("Could not read the file.");

    return GarageStorageMapper.ToGarageData(seedData);
  }
}
