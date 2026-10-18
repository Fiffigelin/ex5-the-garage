using System;
using System.Text.Json.Serialization;
using Garage.Domain.Vehicles;

namespace Garage.Domain;

// stör mig på namnet... vad kan den heta annars
public partial class GaragePlace
{
  private Vehicle[] _vehicles;
  public Guid Id { get; }
  public string Name { get; }
  public int ParkingLots { get; }

  public Vehicle[] Vehicles
  {
    get => _vehicles;
    set
    {
      ArgumentNullException.ThrowIfNull(value);

      if (value.Length > ParkingLots)
      {
        throw new ArgumentException("There's more vehicles, than available parking lots");
      }

      _vehicles = new Vehicle[ParkingLots];
      Array.Copy(value, _vehicles, value.Length);
    }
  }

  public GaragePlace(string name, int parkingLots)
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
  public GaragePlace(Guid id, string name, int parkingLots)
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

  //eller ska den finnas i en serviceklass??
  public Vehicle[] FindCars(string search)
  {
    return IsCarInGarage(search.ToUpper());
  }

  private bool IsGarageFull(Vehicle vehicle)
  {
    if (string.IsNullOrWhiteSpace(vehicle.Id.ToString()))
    {
      throw new ArgumentException("Something went wrong", nameof(vehicle));
    }

    for (int i = 0; i < Vehicles.Length; i++)
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
      if (_vehicles[i].Id == vehicle.Id)
      {
        _vehicles[i] = null!;
        return true;
      }
    }

    return false;
  }

  private Vehicle[] IsCarInGarage(string search)
  {
    Vehicle[] foundVehicles = [];
    for (int i = 0; i < _vehicles.Length; i++)
    {
      Vehicle vehicle = _vehicles[i];

      if (vehicle == null)
      {
        continue;
      }

      if (vehicle.LicensePlate == search ||
          vehicle.Color == search ||
          vehicle.Type.ToString().ToUpper() == search ||
          vehicle.NumberOfWheels.ToString() == search)
      {
        Array.Resize(ref foundVehicles, foundVehicles.Length + 1);

        // IDE rekommenderade 'use index operator'
        // fick google:a [^1] vilket är sista elementet i arrayen, [^2] nästsista osv
        // alltid lär man sig något nytt! :)
        // foundVehicles[foundVehicles.Length - 1] = vehicle;
        foundVehicles[^1] = vehicle;
      }
    }

    return foundVehicles;
  }
}