using System;
using System.Text.Json.Serialization;
using Garage.Domain.Vehicles;

namespace Garage.Domain.Garages;

public partial class ParkingFacility
{
  private Vehicle[] _vehicles;
  public Guid Id { get; }
  public string Name { get; }
  public int ParkingLots { get; }

  public Vehicle[] Vehicles
  {
    // klona arrayen (shallow copy)
    // https://learn.microsoft.com/en-us/dotnet/api/system.array.clone?view=net-10.0
    get => (Vehicle[])_vehicles.Clone();
    set
    {
      ArgumentNullException.ThrowIfNull(value);

      if (value.Length > ParkingLots)
      {
        throw new ArgumentException("There's more vehicles than available parking lots");
      }

      _vehicles = new Vehicle[ParkingLots];
      Array.Copy(value, _vehicles, value.Length);
    }
  }

  public ParkingFacility(string name, int parkingLots)
  {
    if (string.IsNullOrWhiteSpace(name) || name.Length <= 2)
    {
      throw new ArgumentException("Name needs to have atleast 3 chars", nameof(name));
    }

    if (parkingLots <= 0)
    {
      throw new ArgumentException("Parkinglots needs to be more than zero", nameof(parkingLots));
    }

    Id = Guid.NewGuid();
    Name = name;
    ParkingLots = parkingLots;
    _vehicles = new Vehicle[parkingLots];
  }

  [JsonConstructor]
  public ParkingFacility(Guid id, string name, int parkingLots)
  {
    if (string.IsNullOrWhiteSpace(name) || name.Length <= 2)
    {
      throw new ArgumentException("Name needs to have atleast 3 chars", nameof(name));
    }

    if (parkingLots <= 0)
    {
      throw new ArgumentException("Parkinglots needs to be more than zero", nameof(parkingLots));
    }

    Id = id;
    Name = name;
    ParkingLots = parkingLots;
    _vehicles = new Vehicle[parkingLots];
  }

  public void AddVehicle(Vehicle vehicle)
  {
    bool isParked = IsGarageFull(vehicle);

    if (isParked == true)
    {
      ParkingCar(vehicle.Type);
    }
    else
    {
      NoMoreLots(vehicle.Type);
    }
  }

  public void RemoveVehicle(Vehicle vehicle)
  {
    bool hasLeft = RemoveVehicleFromGarage(vehicle);

    if (hasLeft)
    {
      LeaveGarage(vehicle.Type);
    }
    else
    {
      WheresMyCar(vehicle.Type);
    }
  }

  public int ParkedCars()
  {
    return CalculateParkedCars();
  }

  private int CalculateParkedCars()
  {
    int sum = 0;
    foreach (Vehicle v in _vehicles)
    {
      if (v is not null)
      {
        sum++;
      }
    }

    return sum;
  }

  private bool IsGarageFull(Vehicle vehicle)
  {
    if (string.IsNullOrWhiteSpace(vehicle.Id.ToString()))
    {
      throw new ArgumentException("Something went wrong", nameof(vehicle));
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

  private bool RemoveVehicleFromGarage(Vehicle vehicle)
  {
    for (int i = 0; i < _vehicles.Length; i++)
    {
      if (_vehicles[i] is not null && _vehicles[i].Id == vehicle.Id)
      {
        _vehicles[i] = null!;
        return true;
      }
    }

    return false;
  }
}