using System;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;
using static Garage.Service.VehicleService;

namespace Garage.ConsoleUtilities;

public static class VehicleTableConsole
{
  public static void ViewVehiclesTable(ParkingFacility facility)
  {
    int[] columnWidths = [1, 12, 12, 10, 10];
    string[] header = [$"Facility: {facility.Name}", $"Amount of Parkinglots: {facility.ParkingLots}", $"Free Parkinglots: {facility.ParkingLots - facility.ParkedCars()}"];
    string[] columnHeader = ["", "License", "Type", "Color", "Wheels"];

    RowData<Vehicle>[] rows = new RowData<Vehicle>[facility.ParkedCars()];
    Vehicle[] vehicles = new Vehicle[facility.ParkedCars()];
    int index = 0;
    for (int i = 0; i < facility.Vehicles.Length; i++)
    {
      Vehicle vehicle = facility.Vehicles[i];
      if (vehicle is not null)
      {
        // jag veeeeet ingen lambda. Men jag satt i flera
        // onödiga timmar med en generisk form av min TableConsole
        // och jag ville inte snubbla vid mållinjen

        // jag kör for och foreach på varenda lilla array
        rows[index] = GenericTable.FormatDataRow(
          vehicle,
          [
            "",
            $"{vehicle.LicensePlate}",
            $"{vehicle.Type}",
            $"{vehicle.Color}",
            $"{vehicle.NumberOfWheels}"
          ],
          columnWidths,
          () => ViewVehicleInformation(vehicle, facility)
        );

        vehicles[index] = vehicle;
        index++;
      }
    }
    string[] footer = [VehicleStat(vehicles)];

    GenericTable.PrintTable(header, columnHeader, columnWidths, rows, footer);
  }
  public static void ViewSearchTable(VehicleSearchResult[] results)
  {
    int[] columnWidths = [1, -14, -10, -12, -10, 6];
    string[] header = ["Search Results"];
    string[] columnHeader = ["", "Facility", "License", "Type", "Color", "Wheels"];

    RowData<VehicleSearchResult>[] rows = new RowData<VehicleSearchResult>[results.Length];
    Vehicle[] vehicles = new Vehicle[results.Length];

    for (int i = 0; i < results.Length; i++)
    {
      VehicleSearchResult result = results[i];
      {
        rows[i] = GenericTable.FormatDataRow(
            result,
            [
                "",
                result.Facility.Name,
                result.Vehicle.LicensePlate?.Length > 0 ? result.Vehicle.LicensePlate : "",
                result.Vehicle.Type.ToString(),
                result.Vehicle.Color.ToString(),
                result.Vehicle.NumberOfWheels.ToString()
            ],
            columnWidths,
            () => ViewVehicleInformation(
                result.Vehicle,
                result.Facility)
        );

        vehicles[i] = result.Vehicle;
      }
    }

    GenericTable.PrintTable(header, columnHeader, columnWidths, rows, [""]);
  }

  private static void ViewVehicleInformation(Vehicle vehicle, ParkingFacility facility)
  {
    string[] options = ["Exit Parking", "Back"];

    int input = MenuConsole.MenuHandler(["", vehicle.GetInfo()], options);

    switch (input)
    {
      case 0:
        facility.RemoveVehicle(vehicle);
        Console.ReadLine();
        break;
      case 1:
        break;
      default:
        break;
    }
  }

  private static string VehicleStat(Vehicle[] vehicles)
  {
    int car = CountVehicleType(vehicles, VehicleType.Car);
    int bus = CountVehicleType(vehicles, VehicleType.Bus);
    int motorcycle = CountVehicleType(vehicles, VehicleType.Motorcycle);
    int airplane = CountVehicleType(vehicles, VehicleType.Airplane);
    int boat = CountVehicleType(vehicles, VehicleType.Boat);
    return $"{VehicleConsole.PrintVehicleType(VehicleType.Car)} : {car} | {VehicleConsole.PrintVehicleType(VehicleType.Bus)} : {bus} | {VehicleConsole.PrintVehicleType(VehicleType.Motorcycle)} : {motorcycle} | {VehicleConsole.PrintVehicleType(VehicleType.Airplane)} : {airplane} | {VehicleConsole.PrintVehicleType(VehicleType.Boat)} : {boat}";
  }

  private static int CountVehicleType(Vehicle[] vehicles, VehicleType type)
  {
    int sum = 0;
    for (int i = 0; i < vehicles.Length; i++)
    {
      Vehicle vehicle = vehicles[i];

      if (vehicle is not null && vehicle.Type == type)
      {
        sum++;
      }
    }

    return sum;
  }
}