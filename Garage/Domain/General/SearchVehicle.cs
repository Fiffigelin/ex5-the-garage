using System;
using Garage.Domain.Vehicles;

namespace Garage.Domain.General;

public sealed record SearchVehicle(
  string? LicensePlate = null,
  string? Color = null,
  int? NumberOfWheels = null,
  VehicleType? Type = null)
{
  public bool Matches(Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    return TextMatches(vehicle.LicensePlate, LicensePlate)
        && TextMatches(vehicle.Color, Color)
        && (NumberOfWheels is null || vehicle.NumberOfWheels == NumberOfWheels)
        && (Type is null || vehicle.Type == Type);
  }

  // jämförs utan toupper() jämförelse: "abc123", "Abc123" och "ABC123" är samma sak.
  // https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings
  private static bool TextMatches(string? actual, string? wanted)
  {
    if (string.IsNullOrWhiteSpace(wanted))
    {
      return true;
    }

    return string.Equals(actual, wanted.Trim(), StringComparison.OrdinalIgnoreCase);
  }
}