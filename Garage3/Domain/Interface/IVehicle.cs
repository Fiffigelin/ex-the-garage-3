using Garage3.Domain.General;

namespace Garage3.Domain.Interface;

public interface IVehicle
{
  public VehicleType Type { get; }
  public string Color { get; }
  public int NumberOfWheels { get; }
  public string LicensePlate { get; }

  string GetInfo();
}