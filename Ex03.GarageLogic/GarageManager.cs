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
                s_VehiclesCards[licenseNumber].VehicleStatus = eVehicleStatus.InRepair;
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

        public bool InflateVehicleWheelsToMaximum(string i_LicenseNumber, eVehicleStatus i_VehicleStatus)
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

        public bool FuelUpGasolineVehicle(string i_licenseNumber, eFuelType i_FuelType, float i_FuelQuantity)
        {
            bool isFuelUp
        }
    }
}
