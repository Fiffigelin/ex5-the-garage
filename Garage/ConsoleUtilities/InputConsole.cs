using System;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.ConsoleUtilities;

public static class InputConsole
{
  public static string StringInput(string message)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(input))
      {
        return input;
      }

      Console.WriteLine("You need to enter a value. Please try again!");
    }
  }

  public static int IntInput(string message, int max = int.MaxValue)
  {
    while (true)
    {
      Console.Write(message);
      _ = int.TryParse(Console.ReadLine(), out int input);

      if (input > 0 && input <= max)
      {
        return input;
      }

      Console.WriteLine(max == int.MaxValue
        ? $"You need to enter a value between 1 and {max}. Please try again!"
        : "You need to enter a value higher than zero. Please try again!");
    }
  }

  public static bool TextToBool(string message)
  {
    while (true)
    {
      int input = MenuConsole.MenuHandler([message], ["Yes", "No"]);

      switch (input)
      {
        case -1:
          continue;
        case 0:
          return true;
        case 1:
          return false;
        default:
          throw new ArgumentException("Ops, something went wrong!");
      }
    }
  }

  // används vid sökning
  public static string? OptionalStringInput(string message)
  {
    Console.Write(message);
    string? input = Console.ReadLine();

    return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
  }

  public static int? OptionalIntInput(string message)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(input))
      {
        return null;
      }

      if (int.TryParse(input, out int value) && value >= 0)
      {
        return value;
      }

      Console.WriteLine("Enter a whole number (0 or higher), or leave it empty to skip. Please try again!");
    }
  }

  public static string LicenceInput(string message, FacilityData data)
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

      if (!Vehicle.IsValidLicensePlate(input))
      {
        Console.WriteLine(
            "The license plate does not follow Swedish rules. Please try again!");
        continue;
      }

      if (!data.IsLicenseplateUnique(input))
      {
        Console.WriteLine(
            "This license plate is already registered. Please try again!");
        continue;
      }

      return input;
    }
  }

  public static string FacilityNameInput(string message, FacilityData data)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(input) && input.Length >= 3 && data.IsNameUnique(input))
      {
        return input;
      }

      Console.WriteLine("You need to enter a name that is unique and at least 3 characters. Please try again!");
    }
  }
}