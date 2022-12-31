using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public static Dictionary<string, VehicleCard> s_VehiclesCards;

        public Dictionary<string, VehicleCard> GarageCustomer
        {
            get { return s_VehiclesCards; }
            set { s_VehiclesCards = value; }
        }

        public GarageManager()
        {
            s_VehiclesCards = new Dictionary<string, VehicleCard>();
        }

        public void SetNameWheelsProducer(string name)
        {
        }

        public void IsLicenseNumberExist(string licenseNumber, out bool i_IsLicenseNumberExist)
        {
            i_IsLicenseNumberExist = false;
            if (s_VehiclesCards.ContainsKey(licenseNumber))
            {
                i_IsLicenseNumberExist = true;
            }
        }

        public void CreateVehicle(VehicleCardModel i_VehicleCardModel)
        {
            VehicleFactory.CreateVehicle(i_VehicleCardModel);
        }

        public void SetAirPressureForAllWheels(List<Wheel> i_Wheels, float airPressure)
        {
            foreach (Wheel wheel in i_Wheels)
            {
                wheel.CurrentAirPressure = airPressure;
            }
        }

        public void AddVehicleToGarage(VehicleCardModel vehicle)
        {
            VehicleCard vehicleCard = new VehicleCard(vehicle);
            s_VehiclesCards.Add(vehicleCard.Vehicle.LicenseNumber, vehicleCard);
        }

        public List<string> GetGarageLicensesNumbersByFilter(eVehicleStatus? i_VehicleStatusFilter = null)
        {
            List<string> licensesNumbers = new List<string>();
            if (i_VehicleStatusFilter == null)
            {
                foreach (string key in s_VehiclesCards.Keys)
                {
                    licensesNumbers.Add(key);
                }
            }
            else
            {
                licensesNumbers = s_VehiclesCards?.Where(x => x.Value.VehicleStatus == i_VehicleStatusFilter)
                                  .Select(x => x.Key)
                                  .ToList();
            }

            return licensesNumbers;
        }

        public bool ChangeStatusOfVehicle(string i_LicenseNumber, eVehicleStatus i_VehicleStatus)
        {
            bool isChange = false;
            if(s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                s_VehiclesCards[i_LicenseNumber].VehicleStatus = i_VehicleStatus;
                isChange = true;
            }

            return isChange;
        }

        public bool InflateVehicleWheelsToMaximum(string i_LicenseNumber)
        {
            bool isInflate = false;
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                List<Wheel> vehicleWheels = s_VehiclesCards[i_LicenseNumber]?.Vehicle?.Wheels;
                foreach (Wheel wheel in vehicleWheels)
                {
                    wheel.InflateToMax();
                }

                isInflate = true;
            }

            return isInflate;
        }

        public void FuelUpGasolineVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_FuelQuantity)
        {
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                EnergySource tank = s_VehiclesCards[i_LicenseNumber].Vehicle.EnergySource;
                if (tank is ElectricBattery)
                {
                    throw new ArgumentException("Error! The Vehicle Is Electric! The service was not completed.");
                }

                tank.AddEnergy(i_FuelQuantity, i_FuelType);
            }
        }

        public void ChargeElecticVehicle(string i_LicenseNumber, float i_FuelQuantity)
        {
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                EnergySource battery = s_VehiclesCards[i_LicenseNumber].Vehicle.EnergySource;
                if (battery is FuelTank)
                {
                    throw new ArgumentException("Error! The Vehicle Is Not Electric! The service was not completed");
                }

                battery.AddEnergy(i_FuelQuantity, null);
            }
        }

        public List<Tuple<string, object>> GetVehicleInfo(string i_LicenseNumber)
        {
            List<Tuple<string, object>> vehicleInfo = new List<Tuple<string, object>>();
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                VehicleCard vehicleCard = s_VehiclesCards[i_LicenseNumber];
                vehicleInfo.Add(new Tuple<string, object>("Owner Name:", vehicleCard.OwnerName));
                vehicleInfo.Add(new Tuple<string, object>("Owner Phone:", vehicleCard.OwnerPhone));
                vehicleInfo.Add(new Tuple<string, object>("Vehicle Status:", vehicleCard.VehicleStatus));
                vehicleInfo.Add(new Tuple<string, object>("Model Name:", vehicleCard.Vehicle.ModelName));
                vehicleInfo.Add(new Tuple<string, object>("License Number:", vehicleCard.Vehicle.LicenseNumber));
                int wheelNumber = 0;

                foreach (Wheel wheel in vehicleCard.Vehicle.Wheels)
                {
                    string message = $"Wheel number {wheelNumber + 1}: Producer Name: {wheel.ProducerName}, Current Air Pressure: {wheel.CurrentAirPressure}.";
                    vehicleInfo.Add(new Tuple<string, object>(message, null));
                    wheelNumber += 1;
                }

                switch(vehicleCard.Vehicle.EnergySource)
                {
                    case ElectricBattery battery:
                        vehicleInfo.Add(new Tuple<string, object>("Battery current charge:", battery.CurrentEnergy));
                        vehicleInfo.Add(new Tuple<string, object>("Battery max charge is:", battery.MaxEnergyCapacity));
                        break;
                    case FuelTank tank:
                        vehicleInfo.Add(new Tuple<string, object>("Fuel type is:", tank.FuelType));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel current amount is:", tank.CurrentEnergy));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel maximum amount is:", tank.MaxEnergyCapacity));
                        break;
                }

                vehicleInfo.Add(new Tuple<string, object>(vehicleCard.Vehicle.ToString(), null));
            }

            return vehicleInfo;
        }

        public void SetCurrentEnergyStatus(Vehicle i_Vehicle, float i_AirPressure)
        {
            i_Vehicle.EnergySource.CurrentEnergy = i_AirPressure;
        }
    }
}
