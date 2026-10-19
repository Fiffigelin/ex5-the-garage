using System;
using Garage.ConsoleUtilities;
using Garage.Domain.Vehicles;

namespace Garage.Domain.Garages;

public partial class ParkingFacility
{
  protected static void ParkingCar(VehicleType type)
  {
    Console.WriteLine($"Congrats! Your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)} has been parked!");
  }

  protected static void NoMoreLots(VehicleType type)
  {
    Console.WriteLine($"Sorry to inform you but there's no free parkinglot for your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)}");
  }

  protected static void LeaveGarage(VehicleType type)
  {
    Console.WriteLine($"Bye bye, hope we see you soon. Drive safe! {VehicleConsole.PrintVehicleType(type)} 💨--💨--💨");
  }
  protected static void WheresMyCar(VehicleType type)
  {
    Console.WriteLine($"We are so sorry! We seem to have lost your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)}.");
  }
}