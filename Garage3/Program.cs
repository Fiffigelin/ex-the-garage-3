using System;
using System.IO;
using System.Text;
using Garage3.Application;
using Garage3.Data;
using Garage3.Domain.Garages;
using Garage3.UI;

internal class Program
{
  private static void Main(string[] args)
  {
    GarageData data = new();
    VehicleFactory vehicleFactory = new();
    IHandler handler = new GarageHandler(data);
    ConsoleUI ui = new(handler, vehicleFactory);

    FetchSeedData(data);
    ui.Start();
  }
  public static void FetchSeedData(GarageData data)
  {
    string filePath = Path.Combine(
      AppContext.BaseDirectory,
      "Data",
      "data.json"
    );

    GarageSeeder.Seed(data, filePath);
    Console.OutputEncoding = Encoding.UTF8;

  }
}