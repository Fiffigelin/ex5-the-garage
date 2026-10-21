using System;
using Garage.Domain.Garages;
using Garage.Domain.General;
using Garage.Domain.Vehicles;
using static Garage.Service.VehicleService;

namespace Garage.ConsoleUtilities;

public static class VehicleTableConsole
{
  public static void ViewVehiclesTable(Domain.Garages.Garage facility)
  {
    int[] columnWidths = [1, 12, 12, 10, 10];
    string[] columnHeader = ["", "License", "Type", "Color", "Wheels"];

    RowData<Vehicle>[] rows = new RowData<Vehicle>[facility.CountParkedVehicles()];
    Vehicle[] vehicles = new Vehicle[facility.CountParkedVehicles()];
    int index = 0;
    for (int i = 0; i < facility.Vehicles.Length; i++)
    {
      Vehicle? vehicle = facility.Vehicles[i];
      if (vehicle is not null)
      {
        // jag veeeeet ingen lambda. Men jag satt i flera
        // onödiga timmar med en generisk form av min TableConsole
        // och jag ville inte snubbla vid mållinjen

        // jag kör for och foreach på varenda lilla array
        rows[index] = GenericTableConsole.FormatDataRow(
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

    GenericTableConsole.PrintTable(
      () => [
        $"Facility: {facility.Name}",
        $"Amount of Parkinglots: {facility.ParkingLots}",
        $"Free Parkinglots: {facility.ParkingLots - facility.CountParkedVehicles()}"
      ],
      columnHeader,
      columnWidths,
      rows,
      () => [VehicleStat(GetParkedVehicles(facility))]);
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
        rows[i] = GenericTableConsole.FormatDataRow(
            result,
            [
                "",
                result.Garage.Name,
                result.Vehicle.LicensePlate?.Length > 0 ? result.Vehicle.LicensePlate : "",
                result.Vehicle.Type.ToString(),
                result.Vehicle.Color.ToString(),
                result.Vehicle.NumberOfWheels.ToString()
            ],
            columnWidths,
            () => ViewVehicleInformation(
                result.Vehicle,
                result.Garage)
        );

        vehicles[i] = result.Vehicle;
      }
    }

    GenericTableConsole.PrintTable(() => header, columnHeader, columnWidths, rows, () => [""]);
  }
  public static void ViewAllVehicles(VehicleSearchResult[] results)
  {
    int[] columnWidths = [1, -14, -10, -12, -10, 6];
    string[] header = ["Show Vehicles"];
    string[] columnHeader = ["", "Facility", "License", "Type", "Color", "Wheels"];

    RowData<VehicleSearchResult>[] rows = new RowData<VehicleSearchResult>[results.Length];
    Vehicle[] vehicles = new Vehicle[results.Length];

    for (int i = 0; i < results.Length; i++)
    {
      VehicleSearchResult result = results[i];
      {
        rows[i] = GenericTableConsole.FormatDataRow(
            result,
            [
                "",
                result.Garage.Name,
                result.Vehicle.LicensePlate?.Length > 0 ? result.Vehicle.LicensePlate : "",
                result.Vehicle.Type.ToString(),
                result.Vehicle.Color.ToString(),
                result.Vehicle.NumberOfWheels.ToString()
            ],
            columnWidths,
            () => true
        );

        vehicles[i] = result.Vehicle;
      }
    }

    GenericTableConsole.PrintNoIntegrationTable(() => header, columnHeader, columnWidths, rows);
  }

  private static bool ViewVehicleInformation(Vehicle vehicle, Domain.Garages.Garage facility)
  {
    string[] options = ["Exit Parking", "Back"];

    int input = MenuConsole.MenuHandler(["", vehicle.GetInfo(), ""], options);

    switch (input)
    {
      case 0:
        bool removed = facility.RemoveVehicle(vehicle);

        if (removed)
        {
          ParkingMessageConsole.LeaveGarage(vehicle.Type);
        }
        else
        {
          ParkingMessageConsole.WheresMyCar(vehicle.Type);
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);

        return removed;
      case 1:
      case -1:
      default:
        return false;
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

  private static Vehicle[] GetParkedVehicles(Domain.Garages.Garage facility)
  {
    Vehicle?[] allVehicles = facility.Vehicles;
    Vehicle[] parkedVehicles = new Vehicle[facility.CountParkedVehicles()];
    int index = 0;

    for (int i = 0; i < allVehicles.Length; i++)
    {
      if (allVehicles[i] is not null)
      {
        parkedVehicles[index] = allVehicles[i]!;
        index++;
      }
    }

    return parkedVehicles;
  }
}