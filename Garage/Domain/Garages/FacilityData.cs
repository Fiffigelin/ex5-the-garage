using System;
using Garage.Domain.Vehicles;

namespace Garage.Domain.Garages;

public class FacilityData
{
  private ParkingFacility[] _facilities = [];

  public ParkingFacility[] Facilities
  {
    get
    {
      return (ParkingFacility[])_facilities.Clone();
    }
  }

  public void AddFacility(ParkingFacility facility)
  {
    ArgumentNullException.ThrowIfNull(facility);

    Array.Resize(ref _facilities, _facilities.Length + 1);
    _facilities[^1] = facility;
  }

  public bool IsNameUnique(string name)
  {
    foreach (ParkingFacility facility in _facilities)
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

  public ParkingFacility[] AvailableParking()
  {
    ParkingFacility[] available = [];

    foreach (ParkingFacility pf in _facilities)
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

    foreach (ParkingFacility pf in _facilities)
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

