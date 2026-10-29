using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Vehicles;
using Garage3.UI;
using Garage3.UI.Emoji;
using Garage3.UI.Output;
using Garage3.UI.Table;

namespace Garage3.Services;

public static partial class Service
{
  public static void HandleParkingFacilities(GarageData data)
  {
    ConsoleApp.DisplayMainTitle();
    Console.WriteLine();

    while (true)
    {
      List<string> options = ["Add new Garage"];

      foreach (Garage garage in data.Garages)
      {
        options.Add(garage.Name);
      }

      options.Add("Back");

      int input = ConsoleMenu.MenuHandler(["\nHandle parking facilities\n"], options);

      if (input == 0)
      {
        AddNewParkingFacility(data);
      }
      else if (input == options.Count - 1 || input == -1)
      {
        return;
      }
      else
      {
        VehicleTable.ViewVehiclesTable(data.Garages[input - 1]);
      }
    }
  }

  public static void ShowAllParkedVehicles(GarageData data)
  {
    while (true)
    {
      ConsoleApp.DisplayMainTitle();
      List<VehicleSearchResult> results = [];

      foreach (Garage garage in data.Garages)
      {
        foreach (Vehicle vehicle in garage.Vehicles)
        {
          results.Add(new(garage, vehicle));
        }
      }

      VehicleTable.ViewAllVehicles(results);
      ConsoleKey key = Console.ReadKey(true).Key;

      switch (key)
      {
        case ConsoleKey.Escape:
          return;
        default:
          break;
      }
    }
  }

  public static void ParkVehicle(GarageData data)
  {
    while (true)
    {
      ConsoleApp.DisplayMainTitle();
      Console.WriteLine();

      List<Garage> garages = data.AvailableParking();
      List<string> options = [];

      foreach (Garage garage in garages)
      {
        options.Add(garage.Name);
      }
      options.Add("Back");

      int input = ConsoleMenu.MenuHandler(["\nChoose a Parking Facility"], options);

      if (input == -1 || input == options.Count - 1)
      {
        return;
      }

      VehicleType? type = ConsoleEmoji.ChooseVehicleType();
      if (type is null)
      {
        continue;
      }

      Garage parking = garages[input];
      Vehicle? vehicle;

      try
      {
        string licenseplate = ConsoleInput.Read<string>(
          "Licenseplate: ",
          "The license plate must follow Swedish rules and not already be registered. Please try again!",
          ConsoleInput.TryParseUpperString,
          plate => Vehicle.IsValidLicensePlate(plate) && data.IsLicenseplateUnique(plate)
        );

        string color = ConsoleInput.Read<string>(
          "Color: ",
          "You need to enter a value higher than 2 chars. Please try again!",
          ConsoleInput.TryParseString,
          color => color.Length >= 2 && color.Length <= 50
        );

        switch (type)
        {
          case VehicleType.Car:
            vehicle = CreateCar(licenseplate, color);
            break;
          case VehicleType.Motorcycle:
            vehicle = CreateMotorcycle(licenseplate, color);
            break;
          case VehicleType.Bus:
            vehicle = CreateBus(licenseplate, color);
            break;
          case VehicleType.Airplane:
            vehicle = CreateAirplane(licenseplate, color);
            break;
          case VehicleType.Boat:
            vehicle = CreateBoat(licenseplate, color);
            break;
          default:
            throw new ArgumentException("Could not add vehicle. Please try again!");
        }
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"Could not create the vehicle: {ex.Message}");
        ConsoleOutput.PressKeyToContinue();
        continue;
      }

      bool parked = parking.AddVehicle(vehicle);

      if (parked)
      {
        ConsoleOutput.ParkingCar(vehicle.Type);
      }
      else
      {
        ConsoleOutput.NoMoreLots(vehicle.Type);
      }

      ConsoleOutput.PressKeyToContinue();
      return;
    }
  }

  public static void RemoveVehicle(GarageData data)
  {
    ConsoleApp.DisplayMainTitle();

    List<VehicleSearchResult> results = [];
    foreach (Garage garage in data.Garages)
    {
      foreach (Vehicle vehicle in garage.Vehicles)
      {
        results.Add(new VehicleSearchResult(garage, vehicle));
      }
    }

    VehicleTable.ViewVehicleWithGarageNameTable(results, false);
  }

  public static void SearchVehicles(GarageData data)
  {
    ConsoleApp.DisplayMainTitle();
    Console.WriteLine("\nSearch vehicles - leave a field empty to skip it.\n");

    string? licensePlate = ConsoleInput.Read<string?>(
      "Licenseplate: ",
      string.Empty,
      ConsoleInput.TryParseOptionalString
    );

    string? color = ConsoleInput.Read<string?>(
      "Color: ",
      string.Empty,
      ConsoleInput.TryParseOptionalString
    );

    int? numberOfWheels = ConsoleInput.Read<int?>(
      "Number of wheels: ",
      "Enter a whole number (0 or higher), or leave it empty to skip. Please try again!",
      ConsoleInput.TryParseOptionalInt
    );

    VehicleType? type = ConsoleEmoji.ChooseVehicleTypeFilter("Any", ["\nSearch vehicles", "Type of vehicle\n"]);

    SearchVehicle filter = new(licensePlate, color, numberOfWheels, type);
    List<VehicleSearchResult> results = FindVehicles(filter, data);

    VehicleTable.ViewVehicleWithGarageNameTable(results);
  }

  private static void AddNewParkingFacility(GarageData data)
  {
    ConsoleApp.DisplayMainTitle();
    Console.WriteLine("Add New Parking Facility\n");

    string facilityName = ConsoleInput.Read<string>(
      "Name: ",
      "You need to enter a name that is unique and at least 3 characters. Please try again!",
      ConsoleInput.TryParseString,
      value => value.Length >= 3 && data.IsNameUnique(value)
    );

    int parkingLots = ConsoleInput.Read<int>(
      "Amount of parkinglots: ",
      "You need to enter a value of parkinglots higher than 0 but not more than 200. Please try again!",
      int.TryParse,
      value => value > 0 && value <= 200

    );

    try
    {
      data.AddFacility(new Garage(facilityName, parkingLots));
      Console.WriteLine($"\nParking facility '{facilityName}' has been added with {parkingLots} lots");
    }
    catch (ArgumentException ex)
    {
      Console.WriteLine($"\nCould not create the parking facility: {ex.Message}");
    }

    ConsoleOutput.PressKeyToContinue();
  }

  private static List<VehicleSearchResult> FindVehicles(
    SearchVehicle filter,
    GarageData data)
  {
    ArgumentNullException.ThrowIfNull(filter);
    ArgumentNullException.ThrowIfNull(data);

    return [.. data.Garages
      .SelectMany(garage => garage.Vehicles
        .Where(filter.Matches)
        .Select(vehicle => new VehicleSearchResult(garage, vehicle)))];
  }
}