using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.General;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static partial class VehicleService
{

  // En träff i sökningen: fordonet och parkeringshuset det står i.
  public sealed record VehicleSearchResult(ParkingFacility Facility, Vehicle Vehicle);
  public static Vehicle AddVehicle(VehicleType type, FacilityData data)
  {
    string color = InputConsole.StringInput("Color: ");

    switch (type)
    {
      case VehicleType.Car:
        return CreateCar(color, data);
      case VehicleType.Motorcycle:
        return CreateMotorcycle(color, data);
      case VehicleType.Bus:
        return CreateBus(color, data);
      case VehicleType.Airplane:
        return CreateAirplane(color, data);
      case VehicleType.Boat:
        return CreateBoat(color);
      default:
        throw new ArgumentException("Could not add vehicle. Please try again!");
    }
  }

  public static void SearchVehicles(FacilityData data)
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
  private static Vehicle[] SearchVehicle(SearchVehicle search, ParkingFacility facility)
  {
    ArgumentNullException.ThrowIfNull(search);
    ArgumentNullException.ThrowIfNull(facility);

    Vehicle[] vehicles = facility.Vehicles;
    Vehicle[] results = new Vehicle[vehicles.Length];
    int count = 0;

    for (int i = 0; i < vehicles.Length; i++)
    {
      if (vehicles[i] is not null && search.Matches(vehicles[i]))
      {
        results[count] = vehicles[i];
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
      FacilityData data)
  {
    ArgumentNullException.ThrowIfNull(filter);
    ArgumentNullException.ThrowIfNull(data);

    VehicleSearchResult[] hits = [];

    foreach (ParkingFacility facility in data.Garages)
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