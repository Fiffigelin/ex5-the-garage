
using System;
using System.IO;
using System.Text.Json;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.Data;

public static class GarageSeeder
{
  public static void Seed(GarageData garageData, string filePath)
  {
    string json = File.ReadAllText(filePath);

    JsonSerializerOptions options = new()
    {
      PropertyNameCaseInsensitive = true
    };

    GarageSeedData seedData =
        JsonSerializer.Deserialize<GarageSeedData>(json, options)
        ?? throw new InvalidDataException(
            "Kunde inte läsa garagefilen.");

    foreach (GarageSeedItem item in seedData.Garages)
    {
      Domain.Garages.Garage garage = new(item.Name, item.ParkingLots);

      foreach (VehicleSeedItem vehicleItem in item.Vehicles)
      {
        Vehicle vehicle = VehicleFactory.Create(vehicleItem);

        if (!garage.AddVehicle(vehicle))
        {
          throw new InvalidDataException(
              $"Kunde inte parkera fordon i {item.Name}.");
        }
      }

      garageData.AddFacility(garage);
    }
  }
}
