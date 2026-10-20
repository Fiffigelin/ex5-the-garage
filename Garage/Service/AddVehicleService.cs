using Garage.ConsoleUtilities;
using Garage.Domain.Garages;
using Garage.Domain.Vehicles;

namespace Garage.Service;


public static partial class VehicleService
{
  private static Vehicle CreateCar(string color, int numberOfWheels, FacilityData data)
  {
    string carLicenseplate = CreateLicenseplate(data);
    int numberOfDoors = InputConsole.IntInput("Number of doors: ");
    bool isElectric = InputConsole.TextToBool(["Is the vehicle electric?"], ["Yes", "No"]);
    return new Car(color, numberOfWheels, carLicenseplate, numberOfDoors, isElectric);
  }
  private static Vehicle CreateBus(string color, int numberOfWheels, FacilityData data)
  {
    string busLicenseplate = InputConsole.LicenceInput("Licenseplate: ", data);
    int numberOfSeats = InputConsole.IntInput("Number of seats: ");
    return new Bus(color, numberOfWheels, busLicenseplate, numberOfSeats);
  }

  private static string CreateLicenseplate(FacilityData data)
  {
    return InputConsole.LicenceInput("Licenseplate: ", data);
  }
}