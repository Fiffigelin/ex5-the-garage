using System;

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
}