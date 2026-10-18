using System;
using Garage.ConsoleUtilities;
using Garage.Domain;
using Garage.Domain.Vehicles;

internal class Program
{
  private static void Main(string[] args)
  {
    GaragePlace garage = new("Centralt", 2);

    // Console.WriteLine("Fordon som önskas pakeras: Bil");
    // string color = StringNullableHandling("Färg på bilen: ");
    // int numberOfWheels = IntNullableHandling("Antal däck: ");
    // string licensePlate = StringNullableHandling("Registreringsskylt: ");
    // int numberOfDoors = IntNullableHandling("Antal dörrar: ");

    // bool isElctric = BoolNullableHandling(["Fordon som önskas parkeras: Bil", $"Färg på bilen: {color}", $"Antal däck: {numberOfWheels}", $"Registreringsskylt: {licensePlate}", $"Antal dörrar: {numberOfDoors}", "Är bilen elektrisk?"], ["Ja", "Nej"]);

    // Console.WriteLine($"Färg: {color}, Antal däck: {numberOfWheels}, Registreringsskylt: {licensePlate}, Antal dörrar: {numberOfDoors}, Är elektrisk: {isElctric}");

    Car car = new("röd", 4, "abc123", 5, false);
    Car car2 = new("blå", 3, "bcd234", 3, true);
    Car car3 = new("vit", 4, "cde345", 5, true);
    try
    {
      // Car car = new(color, numberOfWheels, licensePlate, numberOfDoors, isElctric);
      garage.AddVehicle(car);
      garage.AddVehicle(car2);
      garage.AddVehicle(car3);
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
    garage.RemoveVehicle(car3);
    garage.RemoveVehicle(car2);
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
}