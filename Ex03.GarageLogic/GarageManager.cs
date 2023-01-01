using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private static Dictionary<string, VehicleCard> s_VehiclesCards;

        public GarageManager()
        {
            s_VehiclesCards = new Dictionary<string, VehicleCard>();
        }

        public Dictionary<string, VehicleCard> GarageCustomer
        {
            get { return s_VehiclesCards; }
            set { s_VehiclesCards = value; }
        }

        public static void GetEnumMinMax<T>(out int o_Min, out int o_Max)
        {
            o_Min = Enum.GetValues(typeof(T)).Cast<int>().Min();
            o_Max = Enum.GetValues(typeof(T)).Cast<int>().Max();
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

        public void SetAirPressureForAllWheels(List<Wheel> i_Wheels, float i_AirPressure)
        {
            foreach (Wheel wheel in i_Wheels)
            {
                wheel.CurrentAirPressure = i_AirPressure;
            }
        }

        public void AddVehicleToGarage(VehicleCardModel i_VehicleModel)
        {
            VehicleCard vehicleCard = new VehicleCard(i_VehicleModel.Vehicle, i_VehicleModel.OwnerName, i_VehicleModel.OwnerPhoneNumber);
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
            bool isInflate;
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                List<Wheel> vehicleWheels = s_VehiclesCards[i_LicenseNumber]?.Vehicle?.Wheels;
                foreach (Wheel wheel in vehicleWheels)
                {
                    wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }

                isInflate = true;
            }
            else
            {
                throw new ArgumentException($"Error! The License Number You Entered ({i_LicenseNumber}) Doesn't Exist! The service was not completed.");
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
            else
            {
                throw new ArgumentException($"Error! The License Number You Entered ({i_LicenseNumber}) Doesn't Exist! The service was not completed.");
            }
        }

        public void ChargeElecticVehicle(string i_LicenseNumber, int i_MinutesToCharge)
        {
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                EnergySource battery = s_VehiclesCards[i_LicenseNumber].Vehicle.EnergySource;
                if (battery is FuelTank)
                {
                    throw new ArgumentException("Error! The Vehicle Is Not Electric! The service was not completed");
                }

                float hoursToCharge = i_MinutesToCharge / 60;
                battery.AddEnergy(hoursToCharge, null);
            }
            else
            {
                throw new ArgumentException($"Error! The License Number You Entered ({i_LicenseNumber}) Doesn't Exist! The service was not completed.");
            }
        }

        public int GetMaxChargingMinutes(string i_LicenseNumber)
        {
            int maxChargingMinutes;
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                ElectricBattery electricBattery = s_VehiclesCards[i_LicenseNumber].Vehicle.EnergySource as ElectricBattery;
                if (electricBattery != null)
                {
                    maxChargingMinutes = (int)Math.Ceiling((electricBattery.MaxEnergyCapacity - electricBattery.CurrentEnergy) * 60);
                }
                else
                {
                    throw new ArgumentException("Error! The Vehicle Is Not Electric! The service was not completed");
                }
            }
            else
            {
                throw new ArgumentException($"Error! The License Number You Entered ({i_LicenseNumber}) Doesn't Exist! The service was not completed.");
            }

            return maxChargingMinutes;
        }

        public List<Tuple<string, object>> GetVehicleInfo(string i_LicenseNumber)
        {
            List<Tuple<string, object>> vehicleInfo = new List<Tuple<string, object>>();
            if (s_VehiclesCards.ContainsKey(i_LicenseNumber))
            {
                VehicleCard vehicleCard = s_VehiclesCards[i_LicenseNumber];
                vehicleInfo.Add(new Tuple<string, object>("Owner Name: ", vehicleCard.OwnerName));
                vehicleInfo.Add(new Tuple<string, object>("Owner Phone: ", vehicleCard.OwnerPhone));
                vehicleInfo.Add(new Tuple<string, object>("Vehicle Status: ", vehicleCard.VehicleStatus));
                vehicleInfo.Add(new Tuple<string, object>("Model Name: ", vehicleCard.Vehicle.ModelName));
                vehicleInfo.Add(new Tuple<string, object>("License Number: ", vehicleCard.Vehicle.LicenseNumber));
                int wheelNumber = 0;

                foreach (Wheel wheel in vehicleCard.Vehicle.Wheels)
                {
                    string message = $"Wheel number {wheelNumber + 1}: Producer Name: {wheel.ProducerName}, Current Air Pressure: {wheel.CurrentAirPressure}, Max Air Pressure: {wheel.MaxAirPressure}.";
                    vehicleInfo.Add(new Tuple<string, object>(message, null));
                    wheelNumber += 1;
                }

                switch(vehicleCard.Vehicle.EnergySource)
                {
                    case ElectricBattery battery:
                        vehicleInfo.Add(new Tuple<string, object>("This Vehicle is Electric.", null));
                        vehicleInfo.Add(new Tuple<string, object>("Battery max charge is: ", battery.MaxEnergyCapacity));
                        vehicleInfo.Add(new Tuple<string, object>("Battery current charge: ", battery.CurrentEnergy));
                        vehicleInfo.Add(new Tuple<string, object>("Battery current percentage: ", battery.CurrentEnergyPercentage));
                        break;
                    case FuelTank tank:
                        vehicleInfo.Add(new Tuple<string, object>("This Vehicle is Regular.", null));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel type is: ", tank.FuelType));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel maximum amount is: ", tank.MaxEnergyCapacity));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel current amount is: ", tank.CurrentEnergy));
                        vehicleInfo.Add(new Tuple<string, object>("Fuel current precentage: ", $"{tank.CurrentEnergyPercentage * 100:n2}%"));
                        break;
                }

                vehicleInfo.Add(new Tuple<string, object>(vehicleCard.Vehicle.ToString(), null));
            }
            else
            {
                throw new ArgumentException($"Error! The License Number You Entered ({i_LicenseNumber}) Doesn't Exist! The service was not completed.");
            }

            return vehicleInfo;
        }

        public void SetCurrentEnergyStatus(Vehicle i_Vehicle, float i_CurrentEnergy)
        {
            i_Vehicle.EnergySource.CurrentEnergy = i_CurrentEnergy;
        }
    }
}
