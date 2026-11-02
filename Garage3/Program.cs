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
    GarageRepository repository = new(
      Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "garage-data.json"
      )
    );
    GarageData data = FetchData(repository);

    VehicleFactory vehicleFactory = new();
    IHandler handler = new GarageHandler(data);
    ConsoleUI ui = new(handler, vehicleFactory);
    Console.OutputEncoding = Encoding.UTF8;

    ui.Start();
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