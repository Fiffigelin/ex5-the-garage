using System;
using Garage.Domain.General;

namespace Garage.Domain.Vehicles;

public class Boat : Vehicle
{
  public int NumberOfEngines { get; }

  public Boat(string color, int numberOfWheels, string? licensePlate, int numberOfEngines)
      : base(Guid.NewGuid(), VehicleType.Boat, color, numberOfWheels, licensePlate)
  {
    if (numberOfEngines <= 0)
    {
      throw new ArgumentException("The boat needs 1 or more engines.", nameof(numberOfEngines));
    }
    NumberOfEngines = numberOfEngines;
  }

  public override string GetInfo()
  {
    string info = base.GetInfo();

    return info +
    $"\nNumber of Engines: {NumberOfEngines}";
  }
}