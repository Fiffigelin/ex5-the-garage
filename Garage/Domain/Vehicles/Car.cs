

using System;
using System.Text.Json.Serialization;
using Garage.Domain.General;

namespace Garage.Domain.Vehicles;

public class Car : Vehicle
{
  public int NumberOfDoors { get; }

  public bool IsElectric { get; }

  public Car(string color, int numberOfWheels, string? licensePlate, int numberOfDoors, bool isElectric)
      : base(Guid.NewGuid(), VehicleType.Car, color, numberOfWheels, licensePlate)
  {
    if (numberOfDoors <= 0)
    {
      throw new ArgumentException("Number of doors must be greater than zero.", nameof(numberOfDoors));
    }

    NumberOfDoors = numberOfDoors;
    IsElectric = isElectric;
  }

  [JsonConstructor]
  public Car(Guid id, string color, int numberOfWheels, string? licensePlate, int numberOfDoors, bool isElectric)
      : base(id, VehicleType.Car, color, numberOfWheels, licensePlate)
  {
    if (numberOfDoors <= 0)
    {
      throw new ArgumentException("Number of doors must be greater than zero.", nameof(numberOfDoors));
    }

    NumberOfDoors = numberOfDoors;
    IsElectric = isElectric;
  }

  public override string GetInfo()
  {
    string boolString = IsElectric ? "Yes" : "No";
    string info = base.GetInfo();

    return info +
    $"\nAmount of doors: {NumberOfDoors}" +
    $"\nElectric: {boolString}";
  }
}