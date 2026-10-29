using System;
using System.Collections.Generic;
using Garage3.Domain.Garages;
using Garage3.UI.Output;
using Garage3.Services;

namespace Garage3.UI;

public static class ConsoleApp
{
  public static void MainMenu(GarageData data)
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

        int choice = ConsoleMenu.MenuHandler(["Welcome to Elins Parking Solution", "Parky Squeezy, Nice and Easy!\n"], mainOptions);

        switch (choice)
        {
          case -1:
            Environment.Exit(0);
            break;
          case 0:
            Service.HandleParkingFacilities(data);
            break;
          case 1:
            Service.ShowAllParkedVehicles(data);
            break;
          case 2:
            Service.ParkVehicle(data);
            break;
          case 3:
            Service.RemoveVehicle(data);
            break;
          case 4:
            Service.SearchVehicles(data);
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
}