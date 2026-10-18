using System;

namespace Garage.ConsoleUtilities;

public static class MenuConsole
{

  public static int MenuHandler(string[] previousTexts, string[] options)
  {
    int selectedIndex = 0;
    ConsoleKey consoleKey;

    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();

      foreach (string text in previousTexts)
      {
        Console.WriteLine(text);
      }

      for (int i = 0; i < options.Length; i++)
      {
        if (i == selectedIndex)
        {
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.WriteLine($" > {options[i]} ");
          Console.ResetColor();
        }
        else
        {
          Console.WriteLine($"  {options[i]} ");
        }
      }

      Console.WriteLine();
      Console.WriteLine("Använd upp och nedpilarna.");
      consoleKey = Console.ReadKey(true).Key;

      switch (consoleKey)
      {
        case ConsoleKey.UpArrow:
          selectedIndex--;

          if (selectedIndex < 0)
          {
            selectedIndex = options.Length - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= options.Length)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:
          return selectedIndex;
        default:
          Console.WriteLine("Använd Enter för att välja.");
          break;
      }

    }
  }
}
