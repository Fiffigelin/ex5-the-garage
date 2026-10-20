using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Garage.Domain.Vehicles;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Car), "Car")]

public abstract class Vehicle
{
  // regex för svenska registeringsskyltar
  // Jag kan INTE regex, här har jag tagit hjälp av ai
  private const string LicensePlatePattern =
      @"^[A-HJ-NPR-UW-Z]{3}[0-9]{2}[0-9A-HJ-NPR-UW-Z]$";
  public Guid Id { get; }

  // denna sätts i barn-klasserna
  [JsonIgnore]
  public VehicleType Type { get; }
  public string Color { get; }
  public int NumberOfWheels { get; }
  public string? LicensePlate { get; }

  // kan nås av sina barnklasser men man ska inte kunna skapa en instans av Vehicle
  protected Vehicle(Guid id, VehicleType type, string color, int numberOfWheels, string? licensePlate)
  {
    if (string.IsNullOrWhiteSpace(color))
    {
      throw new ArgumentException("Color cannot be null or empty.", nameof(color));
    }

    if (type != VehicleType.Boat && !Regex.IsMatch(licensePlate?.ToUpper() ?? string.Empty, LicensePlatePattern))
    {
      throw new ArgumentException("License plate is not in a valid format.", nameof(licensePlate));
    }

    if (type != VehicleType.Boat && numberOfWheels <= 0)
    {
      throw new ArgumentException("Number of wheels must be greater than zero.", nameof(numberOfWheels));
    }

    Id = id;
    Type = type;
    Color = color.ToUpper();
    NumberOfWheels = type == VehicleType.Boat ? 0 : numberOfWheels;
    LicensePlate = licensePlate?.ToUpper();
  }
  public virtual string GetInfo()
  {
    return
    $"{(string.IsNullOrWhiteSpace(LicensePlate) ? "" : $"License plate: {LicensePlate}\n")}" +
    $"Type: {Type}\n" +
    $"Color: {Color}" +
    $"{(NumberOfWheels > 0 ? $"\nWheels: {NumberOfWheels}" : "")}";
  }
}

