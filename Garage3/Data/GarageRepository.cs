using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Garage3.Domain.Garages;

namespace Garage3.Data;

// Denna klass läser data till och från användarens sparfil
public class GarageRepository
{
  private readonly string _filePath;
  // skriver om enum till strängar för lättare felsökning av människor
  // https://stackoverflow.com/questions/63606109/initializing-jsonserializeroptions-with-converters
  private readonly JsonSerializerOptions _options = new()
  {
    WriteIndented = true,
    PropertyNameCaseInsensitive = true,
    Converters =
    {
      new JsonStringEnumConverter()
    }
  };

  public GarageRepository(string filePath)
  {
    _filePath = filePath;
  }

  public GarageData Load()
  {
    if (!File.Exists(_filePath))
    {
      return new GarageData();
    }

    string json = File.ReadAllText(_filePath);

    GarageStorageData storageData =
      JsonSerializer.Deserialize<GarageStorageData>(
        json, _options
      ) ?? throw new InvalidDataException("Could not read the file");

    return GarageStorageMapper.ToGarageData(storageData);
  }

  public void Save(GarageData data)
  {
    GarageStorageData storageData = GarageStorageMapper.ToStorageData(data);

    string json = JsonSerializer.Serialize(storageData, _options);
    File.WriteAllText(_filePath, json);
  }
}