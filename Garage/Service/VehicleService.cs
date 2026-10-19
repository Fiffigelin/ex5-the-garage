using System;
using System.Text.RegularExpressions;
using Garage.ConsoleUtilities;
using Garage.Domain;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static class VehicleService
{
  private const string LicensePlatePattern =
  @"^[A-HJ-NPR-UW-Z]{3}[0-9]{2}[0-9A-HJ-NPR-UW-Z]$";
  public static Vehicle AddVehicle(GaragePlace garage)
  {
    string[] options = [
      $"{VehicleConsole.PrintVehicleType(VehicleType.Car)} Car",
      $"{VehicleConsole.PrintVehicleType(VehicleType.Bus)} Bus"
    ];

    int input = MenuConsole.MenuHandler(["", "Add vehicle to park", "Type of vehicle", ""], options);

    if (input == -1)
    {
      return null!;
    }

    string color = InputConsole.StringInput("Color: ");
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");

    switch (input)
    {
      case 0:
        string carLicenseplate = LicenceInput("Licenseplate: ", garage);
        int numberOfDoors = InputConsole.IntInput("Number of doors: ");
        bool isElectric = InputConsole.TextToBool(["Is the vehicle electric?"], ["Yes", "No"]);
        return new Car(color, numberOfWheels, carLicenseplate, numberOfDoors, isElectric);
      case 1:
        string busLicenseplate = LicenceInput("Licenseplate: ", garage);
        int numberOfSeats = InputConsole.IntInput("Number of seats: ");
        return new Bus(color, numberOfWheels, busLicenseplate, numberOfSeats);
      default:
        throw new ArgumentException("Could not add vehicle. Please try again!");
    }
  }

  private static string LicenceInput(string message, GaragePlace garage)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(input))
      {
        Console.WriteLine("You have to insert a licenseplate. Please try again!");
        continue;
      }

      input = input.Trim().ToUpper();
      Vehicle[] vehicles = SearchVehicle(input, garage);

      if (vehicles.Length == 0 &&
          Regex.IsMatch(input, LicensePlatePattern))
      {
        return input;
      }

      Console.WriteLine("You have to insert a licenseplate. Please try again!");
    }
  }

  // lånat denna från klassen GaragePlace. Jag tänker mer på en serviceklass.
  // dvs om man sak kunna ha flera garage. Fundera lite mer på denna...
  public static Vehicle[] SearchVehicle(string search, GaragePlace garage)
  {
    Vehicle[] foundVehicles = [];
    for (int i = 0; i < garage.Vehicles.Length; i++)
    {
      Vehicle vehicle = garage.Vehicles[i];

      if (vehicle == null)
      {
        continue;
      }

      if (vehicle.LicensePlate == search ||
          vehicle.Color == search ||
          vehicle.Type.ToString().ToUpper() == search ||
          vehicle.NumberOfWheels.ToString() == search)
      {
        Array.Resize(ref foundVehicles, foundVehicles.Length + 1);
        foundVehicles[^1] = vehicle;
      }
    }

    return foundVehicles;
  }

  // behöver fråga Michael om man ska kunna söka på "rö" och få alla röda fordon...
  public static Vehicle[] ContainsSearch(string search, GaragePlace garage)
  {
    Vehicle[] foundVehicles = [];
    for (int i = 0; i < garage.Vehicles.Length; i++)
    {
      Vehicle vehicle = garage.Vehicles[i];

      if (vehicle == null)
      {
        continue;
      }

      if (vehicle.LicensePlate!.Contains(search) ||
          vehicle.Color.Contains(search) ||
          vehicle.Type.ToString().ToUpper().Contains(search) ||
          vehicle.NumberOfWheels.ToString().Contains(search))
      {
        Array.Resize(ref foundVehicles, foundVehicles.Length + 1);
        foundVehicles[^1] = vehicle;
      }
    }
    return foundVehicles;
  }
}