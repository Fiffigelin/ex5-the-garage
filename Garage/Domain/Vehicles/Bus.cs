using System;

namespace Garage.Domain.Vehicles;

public class Bus : Vehicle
{
  public int NumberOfSeats { get; }

  public Bus(string color, int numberOfWheels, string? licensePlate, int numberOfSeats)
      : base(Guid.NewGuid(), VehicleType.Bus, color, numberOfWheels, licensePlate)
  {
    if (numberOfSeats <= 0)
    {
      throw new ArgumentException("Number of seats must be greater than zero.", nameof(numberOfSeats));
    }

    NumberOfSeats = numberOfSeats;
  }

  public override void GetInfo()
  {
    base.GetInfo();
    Console.WriteLine($"Amount of seats: {NumberOfSeats}");
  }
}