using Garage.Domain;

namespace Garage.Utilities;

public static class VehicleConsole
{
  public static void PrintVehicleType(VehicleType vehicle)
  {
    switch (vehicle)
    {
      case VehicleType.Car:
        Console.WriteLine("🚗");
        break;
      case VehicleType.Motorcycle:
        Console.WriteLine("🏍️");
        break;
      case VehicleType.Bus:
        Console.WriteLine("🚌");
        break;
      case VehicleType.Airplane:
        Console.WriteLine("✈️");
        break;
      case VehicleType.Bicycle:
        Console.WriteLine("🚲");
        break;
      case VehicleType.Boat:
        Console.WriteLine("🛥️");
        break;
      default:
        Console.WriteLine("⚠️");
        break;
    }
  }
}