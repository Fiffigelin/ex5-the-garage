using System;
using System.Linq;

namespace Garage.Domain.Garages;

public class FacilityData
{
  private ParkingFacility[] _garages = [];

  public ParkingFacility[] Garages => _garages;

  public void AddFacility(ParkingFacility facility)
  {
    ArgumentNullException.ThrowIfNull(facility);

    Array.Resize(ref _garages, _garages.Length + 1);
    _garages[^1] = facility;
  }

  public bool IsNameUnique(string name)
  {
    return !IsNameInArray(name);
  }

  private bool IsNameInArray(string name)
  {
    return _garages.Any(garage => string.Equals(garage.Name, name, StringComparison.OrdinalIgnoreCase));
  }
}

