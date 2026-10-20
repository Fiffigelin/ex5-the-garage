using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;
using Garage.Service;

internal class Program
{
  private static readonly FacilityData Facilities = new();
  // TODO
  // Gör det möjligt att kolla om ett parkeringshus har plats för fordon
  // se över hur många platser ett visst fordon tar upp 
  private static void Main(string[] args)
  {
    Car car1 = new("vit", 4, "abc123", 5, true);
    Car car2 = new("röd", 4, "bcd123", 5, false);
    Car car3 = new("blå", 4, "bcd12a", 5, false);
    ParkingFacility garage = new("Centralt", 6);
    ParkingFacility garage1 = new("Öster", 6);
    ParkingFacility garage2 = new("Väster", 6);
    garage.AddVehicle(car1);
    garage.AddVehicle(car2);
    garage.AddVehicle(car3);
    Facilities.AddFacility(garage);
    Facilities.AddFacility(garage1);
    Facilities.AddFacility(garage2);

    car3.GetInfo();

    while (true)
    {
      string[] mainOptions =
      {
         "Show Parking Facilities",
         "Park Vehicle",
         "Remove Vehicle",
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
          HandleParkingFacilities();
          break;
        case 1:
          ParkVehicle();
          break;
        case 2:
          Console.WriteLine("Removing vehicle...");
          Console.ReadKey();
          break;
        case 3:
          VehicleService.SearchVehicles(Facilities);
          break;
        case 4:
          Environment.Exit(0);
          break;
      }
    }
  }

  public static void HandleParkingFacilities()
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine();

    ParkingFacilityService.ViewFacilities(Facilities);
  }
  public static void ParkVehicle()
  {
    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();
      Console.WriteLine();

      ParkingFacility[] facilities = Facilities.AvailableParking();

      string[] options = new string[facilities.Length + 1];
      for (int i = 0; i < facilities.Length; i++)
      {
        options[i] = facilities[i].Name;
      }
      options[^1] = "Back";

      int input = MenuConsole.MenuHandler(["\nChoose a Parking Facility"], options);

      if (input == -1 || input == options.Length - 1)
      {
        return;
      }

      VehicleType? type = VehicleConsole.ChooseVehicleType();
      if (type is null)
      {
        continue;
      }

      ParkingFacility parking = facilities[input];
      Vehicle vehicle = VehicleService.AddVehicle((VehicleType)type, Facilities);

      if (vehicle is not null)
      {
        parking.AddVehicle(vehicle);
        Console.ReadLine();
        return;
      }
    }
  }
}