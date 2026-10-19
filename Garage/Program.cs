using System;
using Garage.ConsoleUtilities;
using Garage.Domain;
using Garage.Domain.Vehicles;
using Garage.Service;

internal class Program
{
  // TODO
  // Gör det möjligt att kolla om ett parkeringshus har plats för fordon
  // se över hur många platser ett visst fordon tar upp 
  private static void Main(string[] args)
  {
    Car car1 = new("vit", 4, "abc123", 5, true);
    Car car2 = new("röd", 4, "bcd123", 5, false);
    Car car3 = new("blå", 4, "bcd12a", 5, false);
    Car car4 = new("svart", 4, "efg456", 5, true);
    GaragePlace garage = new("Centralt", 6);
    garage.AddVehicle(car1);
    garage.AddVehicle(car2);
    garage.AddVehicle(car3);
    garage.AddVehicle(car4);

    car3.GetInfo();

    while (true)
    {
      string[] mainOptions =
      {
         "Show all vehicles",
         "Add vehicle",
         "Remove vehicle",
         "Exit"
     };

      int choice = MenuConsole.MenuHandler(["Welcome to Elins parking solution", "Parky squeezy, nice and easy!\n"], mainOptions);

      switch (choice)
      {
        case -1:
          Environment.Exit(0);
          break;
        case 0:
          TableConsole.TableHandler(garage.Vehicles);
          break;

        case 1:
          AddVehicle(garage);
          break;

        case 2:
          Console.WriteLine("Removing vehicle...");
          Console.ReadKey();
          break;

        case 3:
          Environment.Exit(0);
          break;
      }
    }
  }

  public static void AddVehicle(GaragePlace garage)
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine();

    Vehicle vehicle = VehicleService.AddVehicle(garage);

    if (vehicle is not null)
    {
      garage.AddVehicle(vehicle);
      Console.ReadLine();
    }
  }
}