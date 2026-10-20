using System;
using System.Net;
using System.Text.RegularExpressions;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static class VehicleService
{
  private const string LicensePlatePattern =
  @"^[A-HJ-NPR-UW-Z]{3}[0-9]{2}[0-9A-HJ-NPR-UW-Z]$";
  public static Vehicle AddVehicle(VehicleType type, FacilityData data)
  {
    string color = InputConsole.StringInput("Color: ");
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");

    switch (type)
    {
      case VehicleType.Car:
        string carLicenseplate = LicenceInput("Licenseplate: ", data);
        int numberOfDoors = InputConsole.IntInput("Number of doors: ");
        bool isElectric = InputConsole.TextToBool(["Is the vehicle electric?"], ["Yes", "No"]);
        return new Car(color, numberOfWheels, carLicenseplate, numberOfDoors, isElectric);
      case VehicleType.Bus:
        string busLicenseplate = LicenceInput("Licenseplate: ", data);
        int numberOfSeats = InputConsole.IntInput("Number of seats: ");
        return new Bus(color, numberOfWheels, busLicenseplate, numberOfSeats);
      default:
        throw new ArgumentException("Could not add vehicle. Please try again!");
    }
  }

  public static VehicleType? ChooseVehicleType()
  {
    string[] options = [
      $"{VehicleConsole.PrintVehicleType(VehicleType.Car)} Car",
      $"{VehicleConsole.PrintVehicleType(VehicleType.Bus)} Bus",
      "Back"
    ];

    int input = MenuConsole.MenuHandler(["", "Park a Vehicle", "Type of vehicle", ""], options);

    if (input == -1 || input == options.Length - 1)
    {
      return null!;
    }

    switch (input)
    {
      case 0:
        return VehicleType.Car;
      case 1:
        return VehicleType.Bus;
      default:
        throw new ArgumentException("What are you trying to choose?");
    }
  }

  private static string LicenceInput(string message, FacilityData data)
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
      bool isUnique = data.IsLicenseplateUnique(input);

      if (isUnique &&
          Regex.IsMatch(input, LicensePlatePattern))
      {
        return input;
      }

      Console.WriteLine("You have to insert a licenseplate. Please try again!");
    }
  }

  // lånat denna från klassen GaragePlace. Jag tänker mer på en serviceklass.
  // dvs om man sak kunna ha flera garage. Fundera lite mer på denna...
  public static Vehicle[] SearchVehicle(string search, ParkingFacility garage)
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
}