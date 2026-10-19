using System;
using System.Linq;
using Garage.Domain.Vehicles;

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

  public ParkingFacility[] AvailableParking()
  {
    return AvailableParkingFacilites();
  }

  private bool IsNameInArray(string name)
  {
    return _garages.Any(garage => string.Equals(garage.Name, name, StringComparison.OrdinalIgnoreCase));
  }

  private ParkingFacility[] AvailableParkingFacilites()
  {
    ParkingFacility[] park = [];

    foreach (ParkingFacility pf in _garages)
    {
      int occupiedLots = 0;

      for (int i = 0; i < pf.Vehicles.Length; i++)
      {
        if (pf.Vehicles[i] is not null)
        {
          occupiedLots += 1;
        }
      }

      int lots = pf.ParkingLots - occupiedLots;
      if (lots > 0)
      {
        Array.Resize(ref park, park.Length + 1);
        park[^1] = pf;
      }
    }
    return park;
  }
}

