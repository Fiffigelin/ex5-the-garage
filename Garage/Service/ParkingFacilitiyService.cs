using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Garages;

namespace Garage.Service;


public static class ParkingFacilityService
{
  public static void ViewFacilities(FacilityData data)
  {
    while (true)
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
        VehicleTableConsole.ViewVehiclesTable(data.Garages[input - 1]);
      }
    }
  }

  private static void AddNewParkingFacility(FacilityData data)
  {
    TitleConsole.DisplayMainTitle();
    Console.WriteLine("Add New Parking Facility\n");

    string facilityName = InputConsole.FacilityNameInput("Name: ", data);
    int parkingLots = InputConsole.IntInput("Amount of parkinglots: ");

    data.AddFacility(new ParkingFacility(facilityName, parkingLots));
  }
}