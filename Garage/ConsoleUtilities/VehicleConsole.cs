using System;
using Garage.Domain.Vehicles;

namespace Garage.ConsoleUtilities;

public static class VehicleConsole
{
  public static string PrintVehicleType(VehicleType vehicle)
  {
    switch (vehicle)
    {
      case VehicleType.Car:
        return "🚗";
      case VehicleType.Motorcycle:
        return "🏍️";
      case VehicleType.Bus:
        return "🚌";
      case VehicleType.Airplane:
        return "🛩️";
      case VehicleType.Boat:
        return "🛥️";
      default:
        return "⚠️";
    }
  }
}