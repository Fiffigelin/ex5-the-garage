using System;

namespace Garage.Domain.Vehicles;

public class Motorcycle : Vehicle
{
  public bool HasSidecar { get; }

  public Motorcycle(string color, int numberOfWheels, string? licensePlate, bool hasSidecar)
      : base(Guid.NewGuid(), VehicleType.Motorcycle, color, numberOfWheels, licensePlate)
  {
    HasSidecar = hasSidecar;
  }

  public override string GetInfo()
  {
    string boolString = HasSidecar ? "Yes" : "No";
    string info = base.GetInfo();

    return info +
    $"\nHas sidecar: {boolString}";
  }
}