using System;
using System.Text.RegularExpressions;
using Garage.Domain.Garages;

namespace Garage.ConsoleUtilities;

public static class InputConsole
{
  private const string LicensePlatePattern =
@"^[A-HJ-NPR-UW-Z]{3}[0-9]{2}[0-9A-HJ-NPR-UW-Z]$";
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

  public static int IntInput(string message)
  {
    while (true)
    {
      Console.Write(message);
      _ = int.TryParse(Console.ReadLine(), out int input);

      if (input > 0)
      {
        return input;
      }

      Console.WriteLine("You need to enter a value higher than zero. Please try again!");
    }
  }

  public static bool TextToBool(string[] message, string[] options)
  {
    int input = MenuConsole.MenuHandler(message, options);

    switch (input)
    {
      case 0:
        return true;
      case 1:
        return false;
      default:
        throw new ArgumentException("Ops, something went wrong!");
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
      bool isUnique = data.IsLicenseplateUnique(input);

      if (isUnique &&
          Regex.IsMatch(input, LicensePlatePattern))
      {
        return input;
      }

      Console.WriteLine("You have to insert a licenseplate that follows swedish rules and is unique. Please try again!");
    }
  }

  public static string FacilityNameInput(string message, FacilityData data)
  {
    while (true)
    {
      Console.Write(message);
      string? input = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(input) && data.IsNameUnique(input))
      {
        return input;
      }

      Console.WriteLine("You need to enter a name that is unique. Please try again!");
    }
  }
}