using System;
using Garage.ConsoleUtilities;
using Garage.Domain;
using Garage.Domain.Vehicles;

internal class Program
{
  private static void Main(string[] args)
  {
    Car car1 = new("vit", 4, "abc123", 5, true);
    GaragePlace garage = new("Centralt", 2);
    garage.AddVehicle(car1);

    // Console.WriteLine("Fordon som önskas pakeras: Bil");
    // string color = StringNullableHandling("Färg på bilen: ");
    // int numberOfWheels = IntNullableHandling("Antal däck: ");
    // string licensePlate = IsLicenseUnique("Registreringsskylt: ", garage);
    // int numberOfDoors = IntNullableHandling("Antal dörrar: ");

    // bool isElctric = BoolNullableHandling(["Fordon som önskas parkeras: Bil", $"Färg på bilen: {color}", $"Antal däck: {numberOfWheels}", $"Registreringsskylt: {licensePlate}", $"Antal dörrar: {numberOfDoors}", "Är bilen elektrisk?"], ["Ja", "Nej"]);

    // Console.WriteLine($"Färg: {color}, Antal däck: {numberOfWheels}, Registreringsskylt: {licensePlate}, Antal dörrar: {numberOfDoors}, Är elektrisk: {isElctric}");

    Car car = new("röd", 4, "bcd123", 5, false);
    // Car car2 = new("blå", 3, "bcd234", 3, true);
    try
    {
      // Car car = new(color, numberOfWheels, licensePlate, numberOfDoors, isElctric);
      garage.AddVehicle(car);
    }
    catch (Exception e)
    {
      throw new Exception("Något gick fel ", e);
    }
    finally
    {
      Console.WriteLine($"Garage namn: {garage.Name}");
      Console.WriteLine($"Antal parkeringsplatser: {garage.ParkingLots}");

      foreach (var v in garage.Vehicles)
      {
        Console.WriteLine("------------------------------");
        v?.GetInfo();
      }
      Console.WriteLine("------------------------------");
      Console.WriteLine();

    }
    garage.RemoveVehicle(car1);

    string search = StringNullableHandling("Sök ett fordon: ");
    Vehicle[] vehicles = SearchVehicle(search.ToUpper(), garage);

    if (vehicles.Length <= 0)
    {
      Console.WriteLine("------------------------------");
      Console.WriteLine("Could not found any vehicle matching your search");
    }
    foreach (Vehicle v in vehicles)
    {
      Console.WriteLine("------------------------------");
      v?.GetInfo();
    }
    Console.WriteLine("------------------------------");
    Console.WriteLine();

    string searchContains = StringNullableHandling("Sök ett fordon: ");
    Vehicle[] vehicles2 = ContainsSearch(searchContains.ToUpper(), garage);

    if (vehicles.Length <= 0)
    {
      Console.WriteLine("------------------------------");
      Console.WriteLine("Could not found any vehicle matching your search");
    }
    foreach (Vehicle v in vehicles2)
    {
      Console.WriteLine("------------------------------");
      v?.GetInfo();
    }
    Console.WriteLine("------------------------------");
    Console.WriteLine();

  }

  static string StringNullableHandling(string message)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(input))
      {
        return input;
      }

      Console.WriteLine("Kräver svar, försök igen!");
    }
  }
  static int IntNullableHandling(string message)
  {
    while (true)
    {
      Console.Write(message);
      _ = int.TryParse(Console.ReadLine(), out int input);

      if (input > 0)
      {
        return input;
      }

      Console.WriteLine("Kräver svar högre än 0, försök igen!");
    }
  }

  static bool BoolNullableHandling(string[] message, string[] options)
  {
    int input = MenuConsole.MenuHandler(message, options);

    switch (input)
    {
      case 0:
        return true;
      case 1:
        return false;
      default:
        throw new ArgumentException("Ogiltigt svar");
    }
  }

  static string IsLicenseUnique(string message, GaragePlace garage)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();


      if (!string.IsNullOrWhiteSpace(input))
      {
        Vehicle[] vehicles = SearchVehicle(input.ToUpper(), garage);
        if (vehicles.Length == 0)
        {
          return input;
        }
      }

      Console.WriteLine("Kräver svar, försök igen!");
    }
  }

  // lånat denna från klassen GaragePlace. Jag tänker mer på en serviceklass.
  // dvs om man sak kunna ha flera garage. Fundera lite mer på denna...
  static Vehicle[] SearchVehicle(string search, GaragePlace garage)
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
  static Vehicle[] ContainsSearch(string search, GaragePlace garage)
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