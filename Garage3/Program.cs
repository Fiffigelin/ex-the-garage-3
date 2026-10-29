using System;
using System.IO;
using System.Text;
using Garage3.Data;
using Garage3.Domain.Garages;
using Garage3.UI;

internal class Program
{
  private static readonly GarageData GarageData = new();
  private static void Main(string[] args)
  {
    FetchSeedData(GarageData);
    ConsoleApp.MainMenu(GarageData);
  }

  private static void FetchSeedData(GarageData data)
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