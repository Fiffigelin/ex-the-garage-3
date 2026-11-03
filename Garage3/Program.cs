using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Garage3.Application;
using Garage3.Data;
using Garage3.Domain.Garages;
using Garage3.Domain.General;
using Garage3.Domain.Vehicles;
using Garage3.UI;
using Garage3.UI.Emoji;
using Garage3.UI.Table;

internal class Program
{
  private static void Main(string[] args)
  {
    GarageRepository repository = new(
      Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "garage-data.json"
      )
    );
    GarageData data = FetchData(repository);

    VehicleFactory vehicleFactory = new();
    IHandler handler = new GarageHandler(data, repository);
    VehicleTable table = new(handler);
    ConsoleUI ui = new(handler, vehicleFactory, table);
    Console.OutputEncoding = Encoding.UTF8;

    ui.Start();
    // List<MultibleMenuItem<VehicleType>> options =
    // [
    //   new(VehicleType.Car, $"Car {ConsoleEmoji.PrintVehicleType(VehicleType.Car)}"),
    //   new(VehicleType.Motorcycle, $"Motorcycle {ConsoleEmoji.PrintVehicleType(VehicleType.Motorcycle)}"),
    //   new(VehicleType.Bus, $"Bus {ConsoleEmoji.PrintVehicleType(VehicleType.Bus)}"),
    //   new(VehicleType.Boat, $"Boat {ConsoleEmoji.PrintVehicleType(VehicleType.Boat)}"),
    //   new(VehicleType.Airplane, $"Airplane {ConsoleEmoji.PrintVehicleType(VehicleType.Airplane)}")
    // ];

    // List<VehicleType> types = ConsoleMenu.MultibleMenuHandler<VehicleType>(["Choose one or more vehicle types for your garage"], options);

    // foreach (VehicleType type in types)
    // {
    //   Console.WriteLine(type);
    // }
    // ConsoleKey consoleKey = Console.ReadKey(true).Key;
  }
  public static GarageData FetchData(GarageRepository repository)
  {
    if (File.Exists(Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "garage-data.json"
      )))
    {
      return repository.Load();
    }
    else
    {
      string filePath = Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "seed.json"
      );

      GarageData data = GarageSeeder.Seed(filePath);
      repository.Save(data);

      return data;
    }

  }
}