using System;
using System.Text.RegularExpressions;
using Garage3.Domain.General;
using Garage3.Domain.Interface;

namespace Garage3.Domain.Vehicles;

public abstract class Vehicle : IVehicle
{
  // bytte ut mot egen regex som är lite snällare XD
  private const string LicensePlatePattern =
  @"[a-zA-Z]{3}[0-9]{2}[a-zA-Z|0-9]";
  public VehicleType Type { get; }
  public string Color { get; }
  public int NumberOfWheels { get; }
  public string LicensePlate { get; }

  // kan nås av sina barnklasser men man ska inte kunna skapa en instans av Vehicle
  protected Vehicle(VehicleType type, string color, int numberOfWheels, string licensePlate)
  {
    if (string.IsNullOrWhiteSpace(color))
    {
      throw new ArgumentException("Color cannot be null or empty.", nameof(color));
    }

    if (!IsValidLicensePlate(licensePlate))
    {
      throw new ArgumentException("License plate is not in a valid format.", nameof(licensePlate));
    }

    if (type != VehicleType.Boat && numberOfWheels <= 0)
    {
      throw new ArgumentException("Number of wheels must be greater than zero.", nameof(numberOfWheels));
    }

    Type = type;
    Color = color.ToUpper();
    NumberOfWheels = type == VehicleType.Boat ? 0 : numberOfWheels;
    LicensePlate = licensePlate.ToUpper();
  }

  public static bool IsValidLicensePlate(string? licensePlate)
  {
    return !string.IsNullOrWhiteSpace(licensePlate)
        && Regex.IsMatch(
            licensePlate.Trim(),
            LicensePlatePattern,
            RegexOptions.IgnoreCase);
  }
  public virtual string GetInfo()
  {
    string info = string.Empty;

    info += $"Licenseplate: {LicensePlate}\n";
    info += $"Type: {Type}\n";
    info += $"Color: {Color}";

    if (NumberOfWheels > 0)
    {
      info += $"\nWheels: {NumberOfWheels}";
    }

    return info;
  }
}

