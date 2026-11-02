
using System.Collections.Generic;

namespace Garage3.Data;

// Json DTOS
public class GarageStorageData
{
  public List<GarageStorageItem> Garages { get; set; } = [];
}

public class GarageStorageItem
{
  public string Name { get; set; } = "";
  public int ParkingLots { get; set; }
  public List<VehicleStorageItem> Vehicles { get; set; } = [];
}

// Behöver spara alla värden för de klasser som har basklassen Vehicle
public class VehicleStorageItem
{
  public string Type { get; set; } = "";
  public string Color { get; set; } = "";
  public int NumberOfWheels { get; set; }
  public string LicensePlate { get; set; } = "";

  // Car
  public int NumberOfDoors { get; set; }
  public bool IsElectric { get; set; }

  // Motorcycle
  public bool HasSidecar { get; set; }

  // Bus
  public int NumberOfSeats { get; set; }

  // Boat
  public int NumberOfEngines { get; set; }

  // Airplane
  public int NumberOfPassengers { get; set; }
}
