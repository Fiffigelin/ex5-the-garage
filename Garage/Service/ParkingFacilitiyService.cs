using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;

namespace Garage.Service;


public static class ParkingFacilityService
{
  public static void HandleParkingFacilities(GarageData garages)
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine();

    ViewFacilities(garages);
  }

  public static void ViewFacilities(GarageData data)
  {
    while (true)
    {
      Domain.Garages.Garage[] facilities = data.Garages;
      string[] options = ["Add New Parking Facility"];

      foreach (Domain.Garages.Garage facility in facilities)
      {
        Array.Resize(ref options, options.Length + 1);
        options[^1] = facility.Name;
      }
      Array.Resize(ref options, options.Length + 1);
      options[^1] = "Go Back To Main Page";

      int input = MenuConsole.MenuHandler(["\nHandle parking facilities\n"], options);

      if (input == 0)
      {
        AddNewParkingFacility(data);
      }
      else if (input == options.Length - 1 || input == -1)
      {
        return;
      }
      else
      {
        VehicleTableConsole.ViewVehiclesTable(facilities[input - 1]);
      }
    }
  }

  private static void AddNewParkingFacility(GarageData data)
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine("Add New Parking Facility\n");

    string facilityName = InputConsole.FacilityNameInput("Name: ", data);
    int parkingLots = InputConsole.IntInput("Amount of parkinglots: ");

    try
    {
      data.AddFacility(new Domain.Garages.Garage(facilityName, parkingLots));
      Console.WriteLine($"\nParking facility '{facilityName}' has been added with {parkingLots} lots");
    }
    catch (ArgumentException ex)
    {
      Console.WriteLine($"\nCould not create the parking facility: {ex.Message}");
    }

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
  }
}