using System;
using Garage.Domain.Vehicles;

namespace Garage.ConsoleUtilities;

public class TableRow
{
  public Guid VehicleId { get; set; }
  public string Row { get; set; }

  public TableRow(Guid id, string row)
  {
    VehicleId = id;
    Row = row;
  }
}
public static class TableConsole
{
  public static void TableHandler(Vehicle[] vehicles)
  {
    TableRow[] rows = CreateRowOptions(vehicles);
    int selectedIndex = 0;

    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();
      Console.WriteLine("\nChoose with 'Enter' and end with 'Escape'");

      Console.WriteLine($"{"",3}{"License",-12} {"Type",-12} {"Color",-10} {"Wheels",-10}");
      Console.WriteLine(new string('-', 47));

      for (int i = 0; i < rows.Length; i++)
      {
        if (i == selectedIndex)
        {
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.WriteLine($" >{rows[i].Row}");
          Console.ResetColor();
        }
        else
        {
          Console.WriteLine($"  {rows[i].Row}");
        }
      }

      Console.WriteLine(new string('-', 47));



      ConsoleKey consoleKeykey = Console.ReadKey(true).Key;

      switch (consoleKeykey)
      {
        case ConsoleKey.UpArrow:
          selectedIndex--;

          if (selectedIndex < 0)
          {
            selectedIndex = rows.Length - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= rows.Length)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:
          Vehicle vh = FindVehicleById(vehicles, rows[selectedIndex].VehicleId);
          vh.GetInfo();
          Console.WriteLine("\nReturn to table with 'Enter'.");
          Console.ReadLine();
          break;
        case ConsoleKey.Escape:
          return;
        default:
          Console.WriteLine("Wrong key.");
          break;
      }
    }
  }

  private static TableRow[] CreateRowOptions(Vehicle[] vehicles)
  {
    int count = 0;

    foreach (Vehicle? vehicle in vehicles)
    {
      if (vehicle is not null)
      {
        count++;
      }

    }

    if (count == 0)
    {
      Console.WriteLine("No vehicles was found.");
      Console.ReadKey(true);
    }

    TableRow[] rows = new TableRow[count];
    int rowIndex = 0;

    for (int i = 0; i < vehicles.Length; i++)
    {
      if (vehicles[i] is not null)
      {
        string print = $" {vehicles[i].LicensePlate,-12} " +
            $"{VehicleConsole.PrintVehicleType(vehicles[i].Type),-12} " +
            $"{vehicles[i].Color,-10} " +
            $"{vehicles[i].NumberOfWheels,-10}";

        TableRow row = new(vehicles[i].Id, print);
        rows[rowIndex] = row;
        rowIndex++;
      }
    }

    return rows;
  }

  private static Vehicle FindVehicleById(Vehicle[] vehicles, Guid vehicleId)
  {
    foreach (Vehicle vehicle in vehicles)
    {
      if (vehicle is not null && vehicle.Id == vehicleId)
      {
        return vehicle;
      }
    }

    return null!;
  }
}