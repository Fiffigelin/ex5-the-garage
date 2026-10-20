using System;
using Garage.Domain.Vehicles;

namespace Garage.Domain.Garages;

// jag var korkad nog att döpa projektet till Garage XD
public class Garage
{
  private readonly Vehicle?[] _vehicles;
  public Guid Id { get; }
  public string Name { get; }
  public int ParkingLots { get; }
  public int FreeLots => ParkingLots - CountParkedVehicles();
  public bool IsFull => FreeLots == 0;
  public Vehicle?[] Vehicles
  {
    get
    {
      return (Vehicle?[])_vehicles.Clone();
    }
  }
  public const int MaxParkingLots = 200;
  public const int MinNameLength = 3;

  public Garage(string name, int parkingLots)
  {
    if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength)
    {
      throw new ArgumentException("Name needs to have atleast 3 chars", nameof(name));
    }

    if (parkingLots <= 0 || parkingLots > MaxParkingLots)
    {
      throw new ArgumentException("Parkinglots needs to be more than zero and at the max 200", nameof(parkingLots));
    }

    Id = Guid.NewGuid();
    Name = name;
    ParkingLots = parkingLots;
    _vehicles = new Vehicle[parkingLots];
  }

  public bool AddVehicle(Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    for (int i = 0; i < _vehicles.Length; i++)
    {
      if (_vehicles[i]?.Id == vehicle.Id)
      {
        return false;
      }
    }

    for (int i = 0; i < _vehicles.Length; i++)
    {
      if (_vehicles[i] is null)
      {
        _vehicles[i] = vehicle;
        return true;
      }
    }

    return false;
  }

  public bool RemoveVehicle(Vehicle vehicle)
  {
    ArgumentNullException.ThrowIfNull(vehicle);

    for (int i = 0; i < _vehicles.Length; i++)
    {
      if (_vehicles[i]?.Id == vehicle.Id)
      {
        _vehicles[i] = null;
        return true;
      }
    }

    return false;
  }

  public int CountParkedVehicles()
  {
    int sum = 0;
    foreach (Vehicle? v in _vehicles)
    {
      if (v is not null)
      {
        sum++;
      }
    }

    return sum;
  }

  public Vehicle[] ParkedVehicles()
  {
    Vehicle[] result = new Vehicle[CountParkedVehicles()];
    int index = 0;

    foreach (Vehicle? vehicle in _vehicles)
    {
      if (vehicle is not null)
      {
        result[index] = vehicle;
        index++;
      }
    }

    return result;
  }
}