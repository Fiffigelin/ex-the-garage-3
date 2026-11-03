using Garage3.Domain.General;

namespace Garage3.Domain.Vehicles;

public class Motorcycle : Vehicle
{
  public bool HasSidecar { get; }

  public override int ParkingCells => 1;

  public Motorcycle(string color, int numberOfWheels, string licensePlate, bool hasSidecar)
      : base(VehicleType.Motorcycle, color, numberOfWheels, licensePlate)
  {
    HasSidecar = hasSidecar;
  }

  public override string GetInfo()
  {
    string boolString = HasSidecar ? "Yes" : "No";
    string info = base.GetInfo();

    return info +
    $"\nHas sidecar: {boolString}";
  }
}