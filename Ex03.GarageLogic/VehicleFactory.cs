using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelTank;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static void CreateVehicle(VehicleCardModel i_VehicleCardModel)
        {
            List<Wheel> wheels = new List<Wheel>();
            EnergySource energySource = null;
            int numberOfWheels = 0;
            float maxAirPressure = 0f;
            float maxEnergyQuantity = 0f;

            switch (i_VehicleCardModel.VehicleType)
            {
                case eVehicleType.RegularMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    numberOfWheels = 2;
                    maxAirPressure = 28f;
                    break;
                case eVehicleType.RegularCar:
                case eVehicleType.ElectricCar:
                    numberOfWheels = 5;
                    maxAirPressure = 32f;
                    break;
                case eVehicleType.Truck:
                    numberOfWheels = 14;
                    maxAirPressure = 34f;
                    break;
            }

            for (int i = 0; i < numberOfWheels; i++)
            {
                Wheel newTire = new Wheel(maxAirPressure, i_VehicleCardModel.WheelProducerName);
                wheels.Add(newTire);
            }

            switch (i_VehicleCardModel.VehicleType)
            {
                case eVehicleType.RegularMotorcycle:
                    maxEnergyQuantity = 6f;
                    energySource = new FuelTank(eFuelType.Octan98, maxEnergyQuantity);
                    i_VehicleCardModel.Vehicle =
                        new Motorcycle(wheels, energySource, i_VehicleCardModel.ModelName, i_VehicleCardModel.LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    maxEnergyQuantity = 1.6f;
                    energySource = new ElectricBattery(maxEnergyQuantity);
                    i_VehicleCardModel.Vehicle = new Motorcycle(wheels, energySource, i_VehicleCardModel.ModelName, i_VehicleCardModel.LicenseNumber);
                    break;
                case eVehicleType.RegularCar:
                    maxEnergyQuantity = 50f;
                    energySource = new FuelTank(eFuelType.Octan95, maxEnergyQuantity);
                    i_VehicleCardModel.Vehicle = new Car(wheels, energySource, i_VehicleCardModel.ModelName, i_VehicleCardModel.LicenseNumber);
                    break;
                case eVehicleType.ElectricCar:
                    maxEnergyQuantity = 4.7f;
                    energySource = new ElectricBattery(maxEnergyQuantity);
                    i_VehicleCardModel.Vehicle = new Car(wheels, energySource, i_VehicleCardModel.ModelName, i_VehicleCardModel.LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    maxEnergyQuantity = 4.7f;
                    energySource = new FuelTank(eFuelType.Soler, maxEnergyQuantity);
                    i_VehicleCardModel.Vehicle = new Truck(wheels, energySource, i_VehicleCardModel.ModelName, i_VehicleCardModel.LicenseNumber);
                    break;
            }
        }
    }
}
