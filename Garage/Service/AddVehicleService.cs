using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static partial class VehicleService
{
  private static Vehicle CreateCar(string color, GarageData data)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    string licenseplate = InputConsole.LicenceInput("Licenseplate: ", data);
    int numberOfDoors = InputConsole.IntInput("Number of doors: ");
    bool isElectric = InputConsole.TextToBool("Is the vehicle electric?");
    return new Car(color, numberOfWheels, licenseplate, numberOfDoors, isElectric);
  }
  private static Vehicle CreateMotorcycle(string color, GarageData data)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    string licenseplate = InputConsole.LicenceInput("Licenseplate: ", data);
    bool hasSidecar = InputConsole.TextToBool("Has Sidecar:");
    return new Motorcycle(color, numberOfWheels, licenseplate, hasSidecar);
  }
  private static Vehicle CreateBus(string color, GarageData data)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    string licenseplate = InputConsole.LicenceInput("Licenseplate: ", data);
    int numberOfSeats = InputConsole.IntInput("Number of seats: ");
    return new Bus(color, numberOfWheels, licenseplate, numberOfSeats);
  }
  private static Vehicle CreateAirplane(string color, GarageData data)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    string licenseplate = InputConsole.LicenceInput("Licenseplate: ", data);
    int numberOfPassengers = InputConsole.IntInput("Number of Passengers: ");
    return new Airplane(color, numberOfWheels, licenseplate, numberOfPassengers);
  }
  private static Vehicle CreateBoat(string color)
  {
    int numberOfEngines = InputConsole.IntInput("Number of Engines: ");
    return new Boat(color, 0, null, numberOfEngines);
  }
}