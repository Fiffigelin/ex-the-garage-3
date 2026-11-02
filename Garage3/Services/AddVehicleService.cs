// using System;
// using Garage3.Domain.Vehicles;
// using Garage3.UI;

// namespace Garage3.Services;

// public static partial class Service
// {
//   public static Vehicle CreateCar(string license, string color)
//   {
//     int numberOfWheels = NumberOfProperty("Number of wheels: ");
//     int numberOfDoors = NumberOfProperty("Number of doors: ");
//     bool isElectric = TextToBool("Is the vehicle electric?");

//     return new Car(color, numberOfWheels, license, numberOfDoors, isElectric);
//   }
//   public static Vehicle CreateMotorcycle(string license, string color)
//   {
//     int numberOfWheels = NumberOfProperty("Number of wheels: ");
//     bool hasSidecar = TextToBool("Has Sidecar:");

//     return new Motorcycle(color, numberOfWheels, license, hasSidecar);
//   }
//   public static Vehicle CreateBus(string license, string color)
//   {
//     int numberOfWheels = NumberOfProperty("Number of wheels: ");
//     int numberOfSeats = ConsoleInput.Read<int>(
//       "Number of seats: ",
//       "You need to enter a value higher than 0 and no more than 100",
//       int.TryParse,
//       seats => seats > 0 && seats <= 100
//     );

//     return new Bus(color, numberOfWheels, license, numberOfSeats);
//   }
//   public static Vehicle CreateAirplane(string license, string color)
//   {
//     int numberOfWheels = NumberOfProperty("Number of wheels: ");
//     int numberOfPassengers = ConsoleInput.Read<int>(
//       "Number of passengers: ",
//       "You need to enter a value higher than 0 and no more than 300",
//       int.TryParse,
//       passengers => passengers > 0 && passengers <= 300
//     );

//     return new Airplane(color, numberOfWheels, license, numberOfPassengers);
//   }
//   public static Vehicle CreateBoat(string license, string color)
//   {
//     int numberOfEngines = ConsoleInput.Read<int>(
//       "Number of engines: ",
//       "You need to enter a value higher than 0 and no more than 10",
//       int.TryParse,
//       engines => engines > 0 && engines <= 10
//     );

//     return new Boat(color, 0, license, numberOfEngines);
//   }

//   private static int NumberOfProperty(string message)
//   {
//     return ConsoleInput.Read<int>(
//       message,
//       "You need to enter a value higher than 0 and no more than 8. Please try again!",
//       int.TryParse,
//       wheels => wheels > 0 && wheels <= 8
//     );
//   }

//   private static bool TextToBool(string message)
//   {
//     while (true)
//     {
//       int input = ConsoleMenu.MenuHandler([message], ["Yes", "No"]);

//       switch (input)
//       {
//         case -1:
//           continue;
//         case 0:
//           return true;
//         case 1:
//           return false;
//         default:
//           throw new ArgumentException("Ops, something went wrong!");
//       }
//     }
//   }
// }