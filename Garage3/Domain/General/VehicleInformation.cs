using System;
using Garage3.Domain.General;

namespace Garage.Domain.General;

public static class VehicleInfo
{
  public static int GetParkingCells(VehicleType type)
  {
    switch (type)
    {
      case VehicleType.Car:
        return 3;
      case VehicleType.Motorcycle:
        return 1;
      case VehicleType.Bus:
        return 6;
      case VehicleType.Boat:
        return 9;
      case VehicleType.Airplane:
        return 15;
      default:
        throw new ArgumentException("What vehicle did you choose?");
    }
  }
}