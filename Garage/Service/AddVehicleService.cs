using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static partial class VehicleService
{
  private static Vehicle CreateCar(string color, string license)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    int numberOfDoors = InputConsole.IntInput("Number of doors: ");
    bool isElectric = InputConsole.TextToBool("Is the vehicle electric?");
    return new Car(color, numberOfWheels, license, numberOfDoors, isElectric);
  }
  private static Vehicle CreateMotorcycle(string color, string license)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    bool hasSidecar = InputConsole.TextToBool("Has Sidecar:");
    return new Motorcycle(color, numberOfWheels, license, hasSidecar);
  }
  private static Vehicle CreateBus(string color, string license)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    int numberOfSeats = InputConsole.IntInput("Number of seats: ");
    return new Bus(color, numberOfWheels, license, numberOfSeats);
  }
  private static Vehicle CreateAirplane(string color, string license)
  {
    int numberOfWheels = InputConsole.IntInput("Number of wheels: ");
    int numberOfPassengers = InputConsole.IntInput("Number of Passengers: ");
    return new Airplane(color, numberOfWheels, license, numberOfPassengers);
  }
  private static Vehicle CreateBoat(string color, string license)
  {
    int numberOfEngines = InputConsole.IntInput("Number of Engines: ");
    return new Boat(color, 0, license, numberOfEngines);
  }
}