using System;
using Garage3.Domain.General;
using Garage3.UI.Emoji;

namespace Garage3.UI.Output;

public static class ConsoleOutput
{
  public static void ParkingCar(VehicleType type, bool success)
  {
    string message = success == true ? $"Congrats! Your {type.ToString().ToLower()} {ConsoleEmoji.PrintVehicleType(type)} has been parked!"
    : "Could not park vehicle.";

    Console.WriteLine(message);
  }

  public static void NoMoreLots(VehicleType type)
  {
    Console.WriteLine(
        $"Sorry! There's no free parking spot for your {type.ToString().ToLower()} {ConsoleEmoji.PrintVehicleType(type)}.");
  }

  public static void LeaveGarage(VehicleType type)
  {
    Console.WriteLine(
        $"Bye bye, hope we see you soon. Drive safe! {ConsoleEmoji.PrintVehicleType(type)} 💨--💨--💨");
  }

  public static void WheresMyCar(VehicleType type)
  {
    Console.WriteLine(
        $"We're sorry! We couldn't find your {type.ToString().ToLower()} {ConsoleEmoji.PrintVehicleType(type)}.");
  }

  public static void PressKeyToContinue()
  {
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
  }
}