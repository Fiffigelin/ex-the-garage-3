
namespace Garage3.Data;

public class GarageSeedData
{
  public GarageSeedItem[] Garages { get; set; } = [];
}

public class GarageSeedItem
{
  public string Name { get; set; } = "";
  public int ParkingLots { get; set; }
  public VehicleSeedItem[] Vehicles { get; set; } = [];
}

public class VehicleSeedItem
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
