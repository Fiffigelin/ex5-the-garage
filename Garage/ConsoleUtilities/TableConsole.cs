using System;
using System.Linq;
using System.Net.Http.Headers;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.ConsoleUtilities;

public class TableRow
{
  public Vehicle Data { get; set; }
  public string Row { get; set; }

  public TableRow(Vehicle data, string row)
  {
    Data = data;
    Row = row;
  }
}
public static class TableConsole
{
  public static void ViewVehicleTable(ParkingFacility facility)
  {
    int[] widths = [3, 12, 12, 10, 10];
    string header = $"{"",3}{"License",-12} {"Type",-12} {"Color",-10} {"Wheels",-10}";
    TableRow[] rows = CreateVehicleRow(facility.Vehicles, widths);
    int tableWidth = CalculateTableWidth(widths);

    PrintTable(header, rows, facility, tableWidth, true);
  }
  private static void PrintTable(string header, TableRow[] rows, ParkingFacility facility, int width, bool showInfo = false)
  {
    int selectedIndex = 0;

    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();
      Console.WriteLine($"\nParkingfacility: {facility.Name}\nTotal Parkinglots: {facility.ParkingLots}\nAvailable Parkinglots: {facility.ParkingLots - facility.Vehicles.Count(v => v is not null)}\n\nChoose with 'Enter' and end with 'Escape'");

      Console.WriteLine(header);
      Console.WriteLine(new string('-', width));

      if (rows.Length == 0)
      {
        Console.WriteLine("   There's no vehicles parked.");
        Console.WriteLine(new string('-', width));
      }
      else
      {
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
        Console.WriteLine(new string('-', width));

        if (showInfo)
        {
          Vehicle[] data = [];
          foreach (TableRow row in rows)
          {
            Array.Resize(ref data, data.Length + 1);
            data[^1] = row.Data;
          }
          VehicleStat(data);
        }
      }

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

          if (rows.Length == 0)
          {
            break;
          }

          Vehicle data = rows[selectedIndex].Data;
          data.GetInfo();

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

  private static TableRow[] CreateVehicleRow(Vehicle[] vehicles, int[] widths)
  {
    int count = 0;

    foreach (Vehicle vh in vehicles)
    {
      if (vh is not null)
      {
        count++;
      }

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

        TableRow row = new(vehicles[i], print);
        rows[rowIndex] = row;
        rowIndex++;
      }
    }

    return rows;
  }

  private static int CalculateTableWidth(int[] widts)
  {
    int sum = 0;

    foreach (int i in widts)
    {
      // hahaha jag körde utan math.abs först. Kaos!
      sum += Math.Abs(i);
    }

    return sum;
  }

  private static void VehicleStat(Vehicle[] vehicles)
  {
    int cars = vehicles.Count(v => v.Type == VehicleType.Car);
    int busses = vehicles.Count(v => v.Type == VehicleType.Bus);
    int motorcycles = vehicles.Count(v => v.Type == VehicleType.Motorcycle);
    int airplanes = vehicles.Count(v => v.Type == VehicleType.Airplane);
    int boats = vehicles.Count(v => v.Type == VehicleType.Boat);
    Console.WriteLine($"{VehicleConsole.PrintVehicleType(VehicleType.Car)} : {cars} | {VehicleConsole.PrintVehicleType(VehicleType.Bus)} : {busses} | {VehicleConsole.PrintVehicleType(VehicleType.Motorcycle)} : {motorcycles} | {VehicleConsole.PrintVehicleType(VehicleType.Airplane)} : {airplanes} | {VehicleConsole.PrintVehicleType(VehicleType.Boat)} : {boats}");
  }
}