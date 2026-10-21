using System;
using System.IO;
using Garage.Domain.Vehicles;

namespace Garage.Data;

public static class VehicleFactory
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