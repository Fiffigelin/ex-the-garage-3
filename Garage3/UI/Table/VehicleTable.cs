
using System;
using System.Collections.Generic;
using Garage3.Domain.Garages;
using Garage3.Domain.Vehicles;
using Garage3.Domain.General;
using Garage3.UI.Emoji;
using Garage3.UI.Output;
using Garage3.Domain.Interface;
using System.Linq;

namespace Garage3.UI.Table;

public static class VehicleTable
{
  public static void ViewVehiclesTable(Garage garage)
  {
    List<int> columnWidths = [1, 12, 12, 10, 10];
    List<string> columnHeader = ["", "License", "Type", "Color", "Wheels"];

    List<RowData<IVehicle>> rows = [];
    foreach (IVehicle vehicle in garage.Vehicles)
    {
      if (vehicle is not null)
      {
        rows.Add(Table.FormatDataRow(
          vehicle,
          [
            "",
            $"{vehicle.LicensePlate}",
            $"{vehicle.Type}",
            $"{vehicle.Color}",
            $"{vehicle.NumberOfWheels}"
          ],
          columnWidths,
          () => ViewVehicleInformation(vehicle, garage)
        ));
      }
    }

    Table.PrintTable(
      () => [
        $"Facility: {garage.Name}",
        $"Amount of Parkinglots: {garage.ParkingLots}",
        $"Free Parkinglots: {garage.ParkingLots - garage.Vehicles.Count()}"
      ],
      columnHeader,
      columnWidths,
      rows,
      () => [VehicleStat([.. garage.Vehicles])]);
  }
  public static void ViewVehicleWithGarageNameTable(List<VehicleSearchResult> results, bool showStat = false)
  {
    List<int> columnWidths = [1, -14, -10, -12, -10, 6];
    List<string> header = ["Search Results"];
    List<string> columnHeader = ["", "Garage", "License", "Type", "Color", "Wheels"];

    List<RowData<VehicleSearchResult>> rows = [];
    List<Vehicle> vehicles = [];

    foreach (VehicleSearchResult result in results)
    {
      rows.Add(Table.FormatDataRow(
            result,
            [
                "",
                result.Garage.Name,
                result.Vehicle.LicensePlate?.Length > 0 ? result.Vehicle.LicensePlate : "",
                result.Vehicle.Type.ToString(),
                result.Vehicle.Color.ToString(),
                result.Vehicle.NumberOfWheels.ToString()
            ],
            columnWidths,
            () => ViewVehicleInformation(
                result.Vehicle,
                result.Garage)
      ));

      vehicles.Add(result.Vehicle);
    }

    Table.PrintTable(() => header, columnHeader, columnWidths, rows, showStat ? () => [VehicleStat([.. vehicles])] : () => [""]);
  }

  public static void ViewAllVehicles(IEnumerable<VehicleSearchResult> results)
  {
    List<int> columnWidths = [1, -14, -10, -12, -10, 6];
    List<string> header = ["Show Vehicles"];
    List<string> columnHeader = ["", "Facility", "License", "Type", "Color", "Wheels"];

    List<RowData<VehicleSearchResult>> rows = [];
    List<Vehicle> vehicles = [];

    foreach (VehicleSearchResult result in results)
    {
      rows.Add(Table.FormatDataRow(
          result,
          [
              "",
                result.Garage.Name,
                result.Vehicle.LicensePlate?.Length > 0 ? result.Vehicle.LicensePlate : "",
                result.Vehicle.Type.ToString(),
                result.Vehicle.Color.ToString(),
                result.Vehicle.NumberOfWheels.ToString()
          ],
          columnWidths,
          () => true
      ));

      vehicles.Add(result.Vehicle);
    }

    Table.PrintNoIntegrationTable(() => header, columnHeader, columnWidths, rows);
  }

  private static bool ViewVehicleInformation(IVehicle vehicle, IGarage facility)
  {
    List<string> options = ["Exit Parking", "Back"];

    int input = ConsoleMenu.MenuHandler(["", vehicle.GetInfo(), ""], options);

    switch (input)
    {
      case 0:
        bool removed = facility.RemoveVehicle(vehicle.LicensePlate);

        if (removed)
        {
          ConsoleOutput.LeaveGarage(vehicle.Type);
        }
        else
        {
          ConsoleOutput.WheresMyCar(vehicle.Type);
        }

        ConsoleOutput.PressKeyToContinue();

        return removed;
      case 1:
      case -1:
      default:
        return false;
    }
  }

  private static string VehicleStat(List<IVehicle> vehicles)
  {
    int car = 0;
    int bus = 0;
    int motorcycle = 0;
    int airplane = 0;
    int boat = 0;

    foreach (Vehicle vehicle in vehicles)
    {
      switch (vehicle.Type)
      {
        case VehicleType.Car:
          car++;
          break;
        case VehicleType.Bus:
          bus++;
          break;
        case VehicleType.Motorcycle:
          motorcycle++;
          break;
        case VehicleType.Airplane:
          airplane++;
          break;
        case VehicleType.Boat:
          boat++;
          break;
      }
    }

    //refaktorering: lägger till skriven text också då emojisarna inte verkar fungera överallt
    return $"Car {ConsoleEmoji.PrintVehicleType(VehicleType.Car)} : {car} | Bus {ConsoleEmoji.PrintVehicleType(VehicleType.Bus)} : {bus} | Motorcycle {ConsoleEmoji.PrintVehicleType(VehicleType.Motorcycle)} : {motorcycle} | Airplane {ConsoleEmoji.PrintVehicleType(VehicleType.Airplane)} : {airplane} | Boat {ConsoleEmoji.PrintVehicleType(VehicleType.Boat)} : {boat}";
  }
}