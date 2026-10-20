using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;
using Garage.Service;

internal class Program
{
  private static readonly GarageData GarageData = new();
  private static void Main(string[] args)
  {
    Car car1 = new("vit", 4, "abc123", 5, true);
    Car car2 = new("röd", 4, "bcd123", 5, false);
    Car car3 = new("blå", 4, "bcd12a", 5, false);
    Garage.Domain.Garages.Garage garage = new("Centralt", 6);
    Garage.Domain.Garages.Garage garage1 = new("Öster", 6);
    Garage.Domain.Garages.Garage garage2 = new("Väster", 6);
    garage.AddVehicle(car1);
    garage.AddVehicle(car2);
    garage.AddVehicle(car3);
    GarageData.AddFacility(garage);
    GarageData.AddFacility(garage1);
    GarageData.AddFacility(garage2);

    car3.GetInfo();

    while (true)
    {
      try
      {
        string[] mainOptions =
        {
         "Show Parking Facilities",
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
            VehicleService.ParkVehicle(GarageData);
            break;
          case 2:
            VehicleService.SearchVehicles(GarageData);
            break;
          case 3:
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