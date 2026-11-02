using System;
using System.IO;
using Garage3.Domain.Vehicles;

namespace Garage3.Data;

public static class VehicleSeedFactory
{
    public static Vehicle Create(VehicleSeedItem item)
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
}