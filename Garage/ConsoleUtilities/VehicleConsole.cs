using System;
using Garage.Domain.General;

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

  public static VehicleType? ChooseVehicleTypeFilter(string option, string[] headers)
  {
    VehicleType[] types = Enum.GetValues<VehicleType>();

    string[] options = new string[types.Length + 1];
    options[0] = option;

    for (int i = 0; i < types.Length; i++)
    {
      options[i + 1] = $"{PrintVehicleType(types[i])} {types[i]}";
    }

    int input = MenuConsole.MenuHandler(headers, options);

    if (input <= 0 || input > types.Length)
    {
      return null;
    }

    return types[input - 1];
  }

  public static VehicleType? ChooseVehicleType()
  {
    string[] options = [
      $"{PrintVehicleType(VehicleType.Car)} Car",
      $"{PrintVehicleType(VehicleType.Motorcycle)} Motorcycle",
      $"{PrintVehicleType(VehicleType.Bus)} Bus",
      $"{PrintVehicleType(VehicleType.Airplane)} Airplane",
      $"{PrintVehicleType(VehicleType.Boat)} Boat",
      "Back"
    ];

    int input = MenuConsole.MenuHandler(["", "Park a Vehicle", "Type of vehicle", ""], options);

    if (input == -1 || input == options.Length - 1)
    {
      return null;
    }

    switch (input)
    {
      case 0:
        return VehicleType.Car;
      case 1:
        return VehicleType.Motorcycle;
      case 2:
        return VehicleType.Bus;
      case 3:
        return VehicleType.Airplane;
      case 4:
        return VehicleType.Boat;
      default:
        throw new ArgumentException("What are you trying to choose?");
    }
  }
}