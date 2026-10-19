using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;

namespace Garage.Service;


public static class ParkingFacilityService
{
  public static void ViewFacilities(FacilityData data)
  {
    string[] options = ["Add New Parking Facility"];

    foreach (ParkingFacility facility in data.Garages)
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
      ViewFacilities(data.Garages[input - 1]);
    }
  }

  private static void AddNewParkingFacility(FacilityData data)
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine("Add New Parking Facility\n");

    string facilityName = FacilityNameInput("Name: ", data);
    int parkingLots = InputConsole.IntInput("Amount of parkinglots: ");

    data.AddFacility(new ParkingFacility(facilityName, parkingLots));
  }

  private static string FacilityNameInput(string message, FacilityData data)
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

  private static void ViewFacilities(ParkingFacility facility)
  {
    TableConsole.ViewVehicleTable(facility);
  }
}