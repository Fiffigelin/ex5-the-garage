using System;
using System.IO;
using Garage.ConsoleUtilities;
using Garage.Data;
using Garage.Domain.Garages;
using Garage.Service;

internal class Program
{
  private static readonly GarageData GarageData = new();
  private static void Main(string[] args)
  {
    string filePath = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "data.json");

    // Console.WriteLine($"Sökväg: {filePath}");
    // Console.WriteLine($"Filen finns: {File.Exists(filePath)}");
    // Console.WriteLine($"JSON-innehåll:\n{File.ReadAllText(filePath)}");

    GarageSeeder.Seed(GarageData, filePath);

    while (true)
    {
      try
      {
        string[] mainOptions =
        {
         "Show Parking Facilities",
         "Show All Parked Vehicles",
         "Park Vehicle",
         "Search Vehicles",
         "Exit"
        };

        int choice = MenuConsole.MenuHandler(["Welcome to Elins Parking Solution", "Parky Squeezy, Nice and Easy!\n"], mainOptions);

        switch (choice)
        {
          case -1:
            Environment.Exit(0);
            break;
          case 0:
            ParkingFacilityService.HandleParkingFacilities(GarageData);
            break;
          case 1:
            VehicleService.ShowAllParkedVehicles(GarageData);
            break;
          case 2:
            VehicleService.ParkVehicle(GarageData);
            break;
          case 3:
            VehicleService.SearchVehicles(GarageData);
            break;
          case 4:
            Environment.Exit(0);
            break;
        }
      }
      catch (Exception ex)
      {
        Console.Clear();
        Console.WriteLine($"Something unexped happened: {ex.Message}");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey(true);
      }
    }
  }
}