using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ConsoleUIManager
    {
        private static GarageManager s_GarageManager;
        private static VehicleCardModel s_VehicleCardModel;

        public void Run()
        {
            s_GarageManager = new GarageManager();
            s_VehicleCardModel = new VehicleCardModel();
            bool isGarageRunning = true;

            while (isGarageRunning)
            {
                if (!isGarageRunning)
                {
                    Console.WriteLine("Goodbye!");
                    System.Threading.Thread.Sleep(5000);
                }

                selectServiceFromMenu();
                Console.Clear();
            }
            insertNewVehicle();
        }

        private void selectServiceFromMenu()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Welcome to my Garage!!!{Environment.NewLine}Please select wanted service:");
            message.AppendLine("1. Insert new vehicle to the garage");
            message.AppendLine("2. Display license number of vehicles in the garage");
            message.AppendLine("3. Change status of vehicle in the garage");
            message.AppendLine("4. Inflate vehicle wheels to maximum by license number");
            message.AppendLine("5. Fuel up gasoline vehicle in the garage");
            message.AppendLine("6. Charge electric vehicle in the garage");
            message.AppendLine("7. Display full information of a vehicle in the garage");
            message.AppendLine($"Press Q to close the program");
            int selectedService = getIntInput(message, 1, 7);
            runSelectedService(selectedService);
        }

        private void runSelectedService(int selectedService)
        {
            switch (selectedService)
            {
                case 1:
                    selectServiceFromMenu();
                    break;
                case 2:
                    break;

            }
        }

        private void insertNewVehicle()
        {
            const string k_LicenseNumberQuestion = "Please enter the vehicle license number:";
            string licenseNumber = getStringInput(k_LicenseNumberQuestion);
            s_GarageManager.IsLicenseNumberExist(licenseNumber, out bool isLicenseNumberExist);

            if (isLicenseNumberExist)
            {
                Console.WriteLine("The vehicle is already in the garage. Your vehicle status has benn updated to \"In Repair\"");
            }
            else
            {
                s_VehicleCardModel.LicenseNumber = licenseNumber;
                getVehicleInformation();
            }
        }

        private void getVehicleInformation()
        {
            getOwnerName();
            getOwnerPhoneNumber();
            getModelName();
            getWheelsProducer();
            getVehicleType();
            getCurrentEnergyStatus();
            getWheelsAirPressure();
            getUniqueProperties();
            s_GarageManager.AddVehicleToGarage(s_VehicleCardModel);
            Console.WriteLine("The Vehicle Was Successfully Inserted Into The Garage!");
            s_VehicleCardModel = new VehicleCardModel();
        }

        private void getUniqueProperties()
        {
            List<string> messages = s_VehicleCardModel.Vehicle.GetUniquePropertiesMessage();
            List<string> propertiesData = new List<string>();
            messages.ForEach(Console.WriteLine);
            for (int i = 0; i < messages.Count; i++)
            {
                propertiesData.Add(Console.ReadLine());
            }

            s_VehicleCardModel.Vehicle.SetUniquePropertiesData(propertiesData);
        }

        private void getWheelsAirPressure()
        {
            try
            {
                bool v_InvalidAirPressure = true;
                Console.WriteLine("Plesae enter air pressure for all wheels:");
                float airPressure = 0f;
                while (v_InvalidAirPressure)
                {
                    bool isFloat = float.TryParse(Console.ReadLine(), out airPressure);

                    if (isFloat)
                    {
                        s_GarageManager.SetAirPressureForAllWheels(s_VehicleCardModel.Vehicle.Wheels, airPressure);
                        break;
                    }
                }
            }
            catch
            {
                // todo
                    //Console.WriteLine($"Please enter a valid number from {i_MinValue} to {i_MaxValue}");
            }
        }

        private void getCurrentEnergyStatus()
        {
            try
            {
                bool v_InvalidEnergyStatus = true;
                Console.WriteLine("Plesae enter current energy status:");
                float airPressure = 0f;
                while (v_InvalidEnergyStatus)
                {
                    bool isFloat = float.TryParse(Console.ReadLine(), out airPressure);

                    if (isFloat)
                    {
                        //s_GarageManager.SetCurrentEnergyStatus(s_VehicleCardModel.Vehicle.Wheels, airPressure);
                        break;
                    }
                }
            }
            catch
            {
                // todo
                //Console.WriteLine($"Please enter a valid number from {i_MinValue} to {i_MaxValue}");
            }
        }

        private void getVehicleType()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Please select your vehicle type:");
            message.AppendLine("1. RegularMotorcycle");
            message.AppendLine("2. ElectricMotorcycle");
            message.AppendLine("3. RegularCar");
            message.AppendLine("4. ElectricCar");
            message.AppendLine("5. Truck");
            int minValue = 1;
            int maxValue = 5;
            int selectedValue = getIntInput(message, minValue, maxValue);
            s_VehicleCardModel.VehicleType = (eVehicleType)selectedValue;
            s_GarageManager.CreateVehicle(s_VehicleCardModel);
        }


        private void getWheelsProducer()
        {
            const string k_OwnerNameQuestion = "Please enter the wheels producer name:";
            s_VehicleCardModel.WheelProducerName = getStringInput(k_OwnerNameQuestion);
            Console.Clear();
        }

        private void getModelName()
        {
            const string k_OwnerNameQuestion = "Please enter the vehicle model name:";
            s_VehicleCardModel.ModelName = getStringInput(k_OwnerNameQuestion);
            Console.Clear();
        }

        private void getOwnerPhoneNumber()
        {
            const string k_OwnerNameQuestion = "Please enter the owners phone number:";
            s_VehicleCardModel.OwnerPhoneNumber = getStringInput(k_OwnerNameQuestion);
            Console.Clear();
        }

        private void getOwnerName()
        {
            const string k_OwnerNameQuestion = "Please enter the owners name:";
            s_VehicleCardModel.OwnerName = getStringInput(k_OwnerNameQuestion);
            Console.Clear();
        }

        private string getStringInput(string i_Question)
        {
            bool v_IsNotValid = true;
            string userAnswer = string.Empty;
            Console.WriteLine(i_Question);

            while (v_IsNotValid)
            {
                userAnswer = Console.ReadLine();

                if (userAnswer != string.Empty)
                {
                    break;
                }

                Console.WriteLine("Please enter a value!");
            }

            return userAnswer;
        }
        private int getIntInput(StringBuilder i_Message, int i_MinValue, int i_MaxValue)
        {
            bool v_IsNotValid = true;
            Console.WriteLine(i_Message);
            int selectedValue = 0;
            while (v_IsNotValid)
            {
                bool isNumber = int.TryParse(Console.ReadLine(), out selectedValue);

                if (isNumber && selectedValue >= i_MinValue && selectedValue <= i_MaxValue)
                {
                    break;
                }

                Console.WriteLine($"Please enter a valid number from {i_MinValue} to {i_MaxValue}");
            }

            return selectedValue;
        }
    }
}
