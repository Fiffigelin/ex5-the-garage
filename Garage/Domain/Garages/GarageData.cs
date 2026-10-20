using System;
using Garage.Domain.Vehicles;

namespace Garage.Domain.Garages;

public class GarageData
{
  private Garage[] _garages = [];

  public Garage[] Garages
  {
    get
    {
      return (Garage[])_garages.Clone();
    }
  }

  public void AddFacility(Garage facility)
  {
    ArgumentNullException.ThrowIfNull(facility);

    Array.Resize(ref _garages, _garages.Length + 1);
    _garages[^1] = facility;
  }

  public bool IsNameUnique(string name)
  {
    foreach (Garage facility in _garages)
    {
      if (string.Equals(
        facility.Name,
        name,
        StringComparison.OrdinalIgnoreCase
      ))
      {
        return false;
      }
    }

    return true;
  }

  public Garage[] AvailableParking()
  {
    Garage[] available = [];

    foreach (Garage pf in _garages)
    {
      if (!pf.IsFull)
      {
        Array.Resize(ref available, available.Length + 1);
        available[^1] = pf;
      }
    }

    return available;
  }

  public bool IsLicenseplateUnique(string license)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(license);

    foreach (Garage pf in _garages)
    {
      Vehicle[] vehicles = pf.ParkedVehicles();

      foreach (Vehicle? vh in vehicles)
      {
        if (string.Equals(
            vh.LicensePlate,
            license.Trim(),
            StringComparison.OrdinalIgnoreCase))
        {
          return false;
        }
      }
    }

    return true;
  }
}

