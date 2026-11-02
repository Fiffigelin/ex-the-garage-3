using System;
using System.Collections.Generic;
using Garage3.UI.Output;
using Garage3.UI.Interface;
using System.Linq;
using Garage3.UI.Table;
using Garage3.Domain.General;
using Garage3.UI.Emoji;
using Garage3.Domain.Vehicles;
using Garage3.Domain.Garages;
using Garage3.Domain.Interface;
using Garage3.Application;

namespace Garage3.UI;

public class ConsoleUI : IUI
{
  private readonly IHandler _handler;
  private readonly VehicleFactory _vehicleFactory;

  public ConsoleUI(IHandler handler, VehicleFactory vehicleFactory)
  {
    _handler = handler;
    _vehicleFactory = vehicleFactory;
  }

  public void Start()
  {
    MainMenu();
  }
  private void MainMenu()
  {
    while (true)
    {
      try
      {
        List<string> mainOptions =
        [
         "Show Parking Facilities",
         "Show All Parked Vehicles",
         "Park Vehicle",
         "Remove Vehicle",
         "Search",
         "Exit"
        ];

        int choice = ConsoleMenu.MenuHandler(
          [
            "Welcome to Elins Parking Solution",
            "Parky Squeezy, Nice and Easy!\n"
          ],
          mainOptions);

        switch (choice)
        {
          case -1:
            Environment.Exit(0);
            break;
          case 0:
            HandleGarages();
            break;
          case 1:
            ShowAllParkedVehicles();
            break;
          case 2:
            ParkVehicleInGarage();
            break;
          case 3:
            // Service.RemoveVehicle(data);
            break;
          case 4:
            // Service.SearchVehicles(data);
            break;
          case 5:
            Environment.Exit(0);
            break;
        }
      }
      catch (Exception ex)
      {
        Console.Clear();
        Console.WriteLine($"Something unexped happened: {ex.Message}");
        ConsoleOutput.PressKeyToContinue();
        Console.ReadKey(true);
      }
    }
  }

  /* 
* Thank you for visiting https://asciiart.website/
* This ASCII pic can be found at https://asciiart.website/art/5946"
*/
  public static void DisplayMainTitle()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("       .---;-,");
    Console.WriteLine("   __ /_,{)|__;._");
    Console.WriteLine(".''` _     :  _  `.  .:::;.    .::'");
    Console.WriteLine("'--(_)------(_)--' `      '::'");
    Console.ResetColor();
  }

  private void HandleGarages()
  {
    DisplayMainTitle();
    Console.WriteLine();

    while (true)
    {
      var garages = _handler.GetGarages();
      List<string> options = ["Add new Garage"];

      options.AddRange(garages.Select(g => g.Name));
      options.AddRange("Back");

      int input = ConsoleMenu.MenuHandler(
        [
          "\nHandle parking facilities\n"
        ],
        options);

      if (input == 0)
      {
        AddGarage();
      }
      else if (input == options.Count - 1 || input == -1)
      {
        return;
      }
      else
      {
        VehicleTable.ViewVehiclesTable(garages.ElementAt(input - 1));
      }
    }
  }

  private void AddGarage()
  {
    DisplayMainTitle();
    Console.WriteLine("Add New Garage Facility\n");

    string name = ConsoleInput.Read<string>(
      "Name: ",
      "You need to enter a name that is unique and at least 3 characters. Please try again!",
      ConsoleInput.TryParseString,
      value => value.Length >= 3 && _handler.IsGarageNameUnique(value)
    );

    int parkinglots = ConsoleInput.Read<int>(
      "Amount of parkinglots: ",
      "You need to enter a value of parkinglots higher than 0 but not more than 200. Please try again!",
      int.TryParse,
      value => value > 0 && value <= 200

    );

    try
    {
      bool success = _handler.AddGarage(name, parkinglots);
      string message = success ? $"\nParking facility '{name}' has been added with {parkinglots} lots" : "Could not save the garage. Please try again!";
      Console.WriteLine(message);
    }
    catch (ArgumentException ex)
    {
      Console.WriteLine($"\nSomething happened: {ex.Message}");
    }
    finally
    {
      ConsoleOutput.PressKeyToContinue();
    }

  }

  private void ShowAllParkedVehicles()
  {
    while (true)
    {
      DisplayMainTitle();
      var vehicles = _handler.GetAllVehicles();

      VehicleTable.ViewAllVehicles(vehicles);
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

  private void ParkVehicleInGarage()
  {
    while (true)
    {
      DisplayMainTitle();
      Console.WriteLine();
      List<string> options = [];

      while (true)
      {
        var garages = _handler.GetGaragesWithParking().ToList();

        if (!garages.Any())
        {
          Console.WriteLine("No garages with available parking.");
          ConsoleOutput.PressKeyToContinue();
          return;
        }

        options.AddRange(
          garages.Select(g => g.Name)
          .Where(name => !options.Contains(name))
        );
        if (!options.Contains("Back"))
        {
          options.Add("Back");
        }

        int input = ConsoleMenu.MenuHandler(
          ["\nChoose a Garage\n"],
          options);

        if (input == -1 || input == options.Count - 1)
        {
          return;
        }

        Garage selectedGarage = garages[input];

        VehicleType? type = ConsoleEmoji.ChooseVehicleType();

        if (type is null)
        {
          continue;
        }

        IVehicle vehicle = CreateVehicle(type.Value);

        bool success = _handler.ParkVehicle(
          selectedGarage.Name,
          vehicle
        );

        ConsoleOutput.ParkingCar(type.Value, success);
        ConsoleOutput.PressKeyToContinue();
        return;
      }
    }
  }

  private IVehicle CreateVehicle(
    VehicleType type
  )
  {
    string licenseplate = ConsoleInput.Read<string>(
      "Licenseplate: ",
      "The license plate must follow Swedish rules and not already be registered. Please try again!",
      ConsoleInput.TryParseUpperString,
      Vehicle.IsValidLicensePlate);

    string color = ConsoleInput.Read<string>(
      "Color: ",
      "You need to enter a value higher than 2 chars. Please try again!",
      ConsoleInput.TryParseString,
      color => color.Length >= 2 && color.Length <= 50
    );

    switch (type)
    {
      case VehicleType.Car:
        {
          int numberOfWheels = ConsoleInput.NumberOfProperty("Number of wheels: ");
          int numberOfDoors = ConsoleInput.NumberOfProperty("Number of doors: ");
          bool isElectric = ConsoleInput.TextToBool("Is the vehicle electric?");

          return _vehicleFactory.CreateCar(
            licenseplate, color, numberOfWheels, numberOfDoors, isElectric
          );
        }
      case VehicleType.Motorcycle:
        {
          int numberOfWheels = ConsoleInput.NumberOfProperty("Number of wheels: ");
          bool hasSidecar = ConsoleInput.TextToBool("Has sidecar");

          return _vehicleFactory.CreateMotorcycle(
            licenseplate, color, numberOfWheels, hasSidecar
          );
        }
      case VehicleType.Bus:
        {
          int numberOfWheels = ConsoleInput.NumberOfProperty("Number of wheels: ");
          int numberOfSeats = ConsoleInput.NumberOfProperty("Number of seats: ");

          return _vehicleFactory.CreateBus(
            licenseplate, color, numberOfWheels, numberOfSeats
          );
        }
      case VehicleType.Boat:
        {
          int numberOfEnginees = ConsoleInput.NumberOfProperty("Number of enginess: ");

          return _vehicleFactory.CreateBoat(
            licenseplate, color, numberOfEnginees
          );
        }
      case VehicleType.Airplane:
        {
          int numberOfWheels = ConsoleInput.NumberOfProperty("Number of wheels: ");
          int numberOfPassengers = ConsoleInput.NumberOfProperty("Number of passengers: ");

          return _vehicleFactory.CreateAirplane(
            licenseplate, color, numberOfWheels, numberOfPassengers
          );
        }
      default:
        {
          throw new ArgumentException("Unknown vehicle type");
        }
    }
  }
}