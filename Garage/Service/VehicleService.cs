using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.General;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static partial class VehicleService
{

  // En träff i sökningen: fordonet och parkeringshuset det står i.
  public sealed record VehicleSearchResult(Domain.Garages.Garage Garage, Vehicle Vehicle);
  public static void ParkVehicle(GarageData data)
  {
    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();
      Console.WriteLine();

      Domain.Garages.Garage[] garages = data.AvailableParking();

      string[] options = new string[garages.Length + 1];
      for (int i = 0; i < garages.Length; i++)
      {
        options[i] = garages[i].Name;
      }
      options[^1] = "Back";

      int input = MenuConsole.MenuHandler(["\nChoose a Parking Facility"], options);

      if (input == -1 || input == options.Length - 1)
      {
        return;
      }

      VehicleType? type = VehicleConsole.ChooseVehicleType();
      if (type is null)
      {
        continue;
      }

      Domain.Garages.Garage parking = garages[input];
      Vehicle? vehicle;

      try
      {
        vehicle = AddVehicle((VehicleType)type, data);
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"Could not create the vehicle: {ex.Message}");
        Console.WriteLine("Press any key to try again...");
        Console.ReadKey(true);
        continue;
      }

      bool parked = parking.AddVehicle(vehicle);

      if (parked)
      {
        ParkingMessageConsole.ParkingCar(vehicle.Type);
      }
      else
      {
        ParkingMessageConsole.NoMoreLots(vehicle.Type);
      }

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey(true);
      return;
    }
  }

  public static Vehicle AddVehicle(VehicleType type, GarageData garage)
  {
    string color = InputConsole.StringInput("Color: ");

    switch (type)
    {
      case VehicleType.Car:
        return CreateCar(color, garage);
      case VehicleType.Motorcycle:
        return CreateMotorcycle(color, garage);
      case VehicleType.Bus:
        return CreateBus(color, garage);
      case VehicleType.Airplane:
        return CreateAirplane(color, garage);
      case VehicleType.Boat:
        return CreateBoat(color);
      default:
        throw new ArgumentException("Could not add vehicle. Please try again!");
    }
  }

  public static void SearchVehicles(GarageData data)
  {
    Console.Clear();
    TitleConsole.DisplayMainTitle();
    Console.WriteLine("\nSearch vehicles - leave a field empty to skip it.\n");

    string? licensePlate = InputConsole.OptionalStringInput("License plate: ");
    string? color = InputConsole.OptionalStringInput("Color: ");
    int? numberOfWheels = InputConsole.OptionalIntInput("Number of wheels: ");
    VehicleType? type = VehicleConsole.ChooseVehicleTypeFilter();

    SearchVehicle filter = new(licensePlate, color, numberOfWheels, type);
    VehicleSearchResult[] results = SearchVehicle(filter, data);

    VehicleTableConsole.ViewSearchTable(results);
  }

  // söker i enskilt parkeringshus
  private static Vehicle[] SearchVehicle(SearchVehicle search, Domain.Garages.Garage garage)
  {
    ArgumentNullException.ThrowIfNull(search);
    ArgumentNullException.ThrowIfNull(garage);

    Vehicle?[] vehicles = garage.Vehicles;
    Vehicle[] results = new Vehicle[vehicles.Length];
    int count = 0;

    for (int i = 0; i < vehicles.Length; i++)
    {
      Vehicle? vehicle = vehicles[i];

      if (vehicle is not null && search.Matches(vehicle))
      {
        results[count] = vehicle;
        count++;
      }
    }

    Vehicle[] matches = new Vehicle[count];
    Array.Copy(results, matches, count);

    return matches;
  }

  // Söker i ALLA parkeringshus och berättar även vilket hus fordonet står i.
  private static VehicleSearchResult[] SearchVehicle(
      SearchVehicle filter,
      GarageData data)
  {
    ArgumentNullException.ThrowIfNull(filter);
    ArgumentNullException.ThrowIfNull(data);

    VehicleSearchResult[] hits = [];

    foreach (Domain.Garages.Garage facility in data.Garages)
    {
      Vehicle[] vehicles = SearchVehicle(filter, facility);

      foreach (Vehicle vehicle in vehicles)
      {
        Array.Resize(ref hits, hits.Length + 1);
        hits[^1] = new VehicleSearchResult(facility, vehicle);
      }
    }

    return hits;
  }
}