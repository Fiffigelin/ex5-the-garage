
using System;
using Garage.Domain.General;

namespace Garage.ConsoleUtilities;

public static class ParkingMessageConsole
{
  public static void ParkingCar(VehicleType type)
  {
    Console.WriteLine(
        $"Congrats! Your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)} has been parked!");
  }

  public static void NoMoreLots(VehicleType type)
  {
    Console.WriteLine(
        $"Sorry! There's no free parking spot for your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)}.");
  }

  public static void LeaveGarage(VehicleType type)
  {
    Console.WriteLine(
        $"Bye bye, hope we see you soon. Drive safe! {VehicleConsole.PrintVehicleType(type)} 💨--💨--💨");
  }

  public static void WheresMyCar(VehicleType type)
  {
    Console.WriteLine(
        $"We're sorry! We couldn't find your {type.ToString().ToLower()} {VehicleConsole.PrintVehicleType(type)}.");
  }
}
