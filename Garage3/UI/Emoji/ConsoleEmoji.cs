using System;
using System.Collections.Generic;
using Garage3.Domain.General;

namespace Garage3.UI.Emoji;

public static class ConsoleEmoji
{
  public static string PrintVehicleType(VehicleType vehicle)
  {
    switch (vehicle)
    {
      case VehicleType.Car:
        return "🚗";
      case VehicleType.Motorcycle:
        return "🏍️";
      case VehicleType.Bus:
        return "🚌";
      case VehicleType.Airplane:
        return "🛩️";
      case VehicleType.Boat:
        return "🛥️";
      default:
        return "⚠️";
    }
  }

  public static VehicleType? ChooseVehicleTypeFilter(string option, List<string> headers)
  {
    List<VehicleType> types = [.. Enum.GetValues<VehicleType>()];

    List<string> options = [option, $"{PrintVehicleType(VehicleType.Car)} Car",
      $"{PrintVehicleType(VehicleType.Motorcycle)} Motorcycle",
      $"{PrintVehicleType(VehicleType.Bus)} Bus",
      $"{PrintVehicleType(VehicleType.Airplane)} Airplane",
      $"{PrintVehicleType(VehicleType.Boat)} Boat",];

    for (int i = 0; i < types.Count; i++)
    {
      options[i + 1] = $"{PrintVehicleType(types[i])} {types[i]}";
    }

    int input = ConsoleMenu.MenuHandler(headers, options);

    if (input <= 0 || input > types.Count)
    {
      return null;
    }

    return types[input - 1];
  }

  public static VehicleType? ChooseVehicleType()
  {
    List<string> options = [
      $"{PrintVehicleType(VehicleType.Car)} Car",
      $"{PrintVehicleType(VehicleType.Motorcycle)} Motorcycle",
      $"{PrintVehicleType(VehicleType.Bus)} Bus",
      $"{PrintVehicleType(VehicleType.Airplane)} Airplane",
      $"{PrintVehicleType(VehicleType.Boat)} Boat",
      "Back"
    ];

    int input = ConsoleMenu.MenuHandler(["", "Park a Vehicle", "Type of vehicle", ""], options);

    if (input == -1 || input == options.Count - 1)
    {
      return null;
    }

    switch (input)
    {
      case 0:
        return VehicleType.Car;
      case 1:
        return VehicleType.Motorcycle;
      case 2:
        return VehicleType.Bus;
      case 3:
        return VehicleType.Airplane;
      case 4:
        return VehicleType.Boat;
      default:
        throw new ArgumentException("What are you trying to choose?");
    }
  }
}