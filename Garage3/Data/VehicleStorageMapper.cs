using System;
using System.IO;
using Garage3.Domain.Vehicles;

namespace Garage3.Data;

public static class VehicleStorageMapper
{
    public static Vehicle ToVehicle(VehicleStorageItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        return item.Type switch
        {
            "Car" => new Car(
                item.Color,
                item.NumberOfWheels,
                item.LicensePlate,
                item.NumberOfDoors,
                item.IsElectric),

            "Motorcycle" => new Motorcycle(
                item.Color,
                item.NumberOfWheels,
                item.LicensePlate,
                item.HasSidecar),

            "Bus" => new Bus(
                item.Color,
                item.NumberOfWheels,
                item.LicensePlate,
                item.NumberOfSeats),

            "Boat" => new Boat(
                item.Color,
                item.NumberOfWheels,
                item.LicensePlate,
                item.NumberOfEngines),

            "Airplane" => new Airplane(
                item.Color,
                item.NumberOfWheels,
                item.LicensePlate,
                item.NumberOfPassengers),

            _ => throw new InvalidDataException(
                $"Okänd fordonstyp: {item.Type}")
        };
    }

    public static VehicleStorageItem ToStorageItem(Vehicle item)
    {
        ArgumentNullException.ThrowIfNull(item);

        return item switch
        {
            Car car => new VehicleStorageItem
            {
                Type = "Car",
                Color = car.Color,
                NumberOfWheels = car.NumberOfWheels,
                LicensePlate = car.LicensePlate,
                NumberOfDoors = car.NumberOfDoors,
                IsElectric = car.IsElectric
            },

            Motorcycle motorcycle => new VehicleStorageItem
            {
                Type = "Motorcycle",
                Color = motorcycle.Color,
                NumberOfWheels = motorcycle.NumberOfWheels,
                LicensePlate = motorcycle.LicensePlate,
                HasSidecar = motorcycle.HasSidecar
            },

            Bus bus => new VehicleStorageItem
            {
                Type = "Bus",
                Color = bus.Color,
                NumberOfWheels = bus.NumberOfWheels,
                LicensePlate = bus.LicensePlate,
                NumberOfSeats = bus.NumberOfSeats
            },

            Boat boat => new VehicleStorageItem
            {
                Type = "Boat",
                Color = boat.Color,
                NumberOfWheels = boat.NumberOfWheels,
                LicensePlate = boat.LicensePlate,
                NumberOfEngines = boat.NumberOfEngines
            },

            Airplane airplane => new VehicleStorageItem
            {
                Type = "Airplane",
                Color = airplane.Color,
                NumberOfWheels = airplane.NumberOfWheels,
                LicensePlate = airplane.LicensePlate,
                NumberOfPassengers = airplane.NumberOfPassengers
            },

            _ => throw new InvalidDataException(
                $"Unknown vehicle type: {item.GetType().Name}")
        };
    }
}