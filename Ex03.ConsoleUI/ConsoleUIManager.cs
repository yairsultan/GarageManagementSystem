using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.ConsoleUI
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
                selectServiceFromMenu();
                isGarageRunning = askIfGarageRunning();
                Console.Clear();
            }

            Console.WriteLine("Thank you for visiting my garage, good bye!");
            System.Threading.Thread.Sleep(5000);
        }

        private bool askIfGarageRunning()
        {
            bool isGarageRunning = false;
            const string k_ReturnToMenuQuestion = "Please enter 'Q' to quit or any other key to return back to select service";
            string selection = getStringInput(Environment.NewLine + k_ReturnToMenuQuestion);
            if (selection != "Q")
            {
                isGarageRunning = true;
            }

            return isGarageRunning;
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
            Console.Clear();
            runSelectedService(selectedService);
        }

        private void runSelectedService(int selectedService)
        {
            switch (selectedService)
            {
                case 1:
                    insertNewVehicle();
                    break;
                case 2:
                    displayLicensesInGarage();
                    break;
                case 3:
                    changeVehicleStatus();
                    break;
                case 4:
                    inflateWheelsToMax();
                    break;
                case 5:
                    fuelUpGasolineVheicle();
                    break;
                case 6:
                    chargeElectricVeicle();
                    break;
                case 7:
                    displayFullVehicleInformation();
                    break;
            }
        }

        private void displayFullVehicleInformation()
        {
            string licenseNumber = getLicenseNumber();
            StringBuilder message = new StringBuilder();
            try
            {
                List<Tuple<string, object>> vehicleInfo = s_GarageManager.GetVehicleInfo(licenseNumber);
                foreach (Tuple<string, object> vehicleInfoTuple in vehicleInfo)
                {
                    message.Append(vehicleInfoTuple.Item1);
                    if (vehicleInfoTuple.Item2 != null)
                    {
                        message.Append(vehicleInfoTuple.Item2.ToString());
                    }

                    message.AppendLine();
                }

                Console.WriteLine(message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private void chargeElectricVeicle()
        {
            string licenseNumber = getLicenseNumber();
            StringBuilder message = new StringBuilder();
            message.AppendLine("Please enter the amount of minutes you want to charge:");
            try
            {
                int maxChargingMinutes = s_GarageManager.GetMaxChargingMinutes(licenseNumber);
                int chargeMinutes = getIntInput(message, 0, maxChargingMinutes);
                s_GarageManager.ChargeElecticVehicle(licenseNumber, chargeMinutes);
                Console.WriteLine("The vehicle was successfully charged.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private void inflateWheelsToMax()
        {
            string licenseNumber = getLicenseNumber();
            try
            {
                bool isInflated = s_GarageManager.InflateVehicleWheelsToMaximum(licenseNumber);
                if (!isInflated)
                {
                    Console.WriteLine("Something went wrong.");
                }
                else
                {
                    Console.WriteLine("All of your wheels are inflated to their max.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private void fuelUpGasolineVheicle()
        {
            string licenseNumber = getLicenseNumber();
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Please select the fuel type:{Environment.NewLine}");
            int index = 1;
            foreach (string status in Enum.GetNames(typeof(eFuelType)))
            {
                message.AppendLine($"{index++}. {status}");
            }

            GarageManager.GetEnumMinMax<eFuelType>(out int minEnumValue, out int maxEnumValue);
            int fuelType = getIntInput(message, minEnumValue, maxEnumValue);
            message.Clear();
            message.AppendLine("Please enter the amount of fuel you want to add:");
            float fuelQuantity = getFloatInput(message);
            try
            {
                s_GarageManager.FuelUpGasolineVehicle(licenseNumber, (eFuelType)fuelType, fuelQuantity);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenseNumber();
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Please select the vehicles new status:{Environment.NewLine}");
            int index = 1;
            foreach (string status in Enum.GetNames(typeof(eVehicleStatus)))
            {
                message.AppendLine($"{index++}. {status}");
            }

            Console.Clear();
            Console.WriteLine(message);
            GarageManager.GetEnumMinMax<eVehicleStatus>(out int minEnumValue, out int maxEnumValue);
            message.Clear();
            int selectedstatus = getIntInput(message, minEnumValue, maxEnumValue);
            bool isLicenseExist = s_GarageManager.ChangeStatusOfVehicle(licenseNumber, (eVehicleStatus)selectedstatus);
            if (!isLicenseExist)
            {
                Console.WriteLine("No license number found.");
            }
            else
            {
                Console.WriteLine($"Your vehicle status has been updated to {(eVehicleStatus)selectedstatus} status.");
            }
        }

        private string getLicenseNumber()
        {
            const bool v_InvalidLicenseNumber = true;
            const string k_LicenseNumberQuestion = "Please enter your vehicles license number:";
            string licenseNumber = string.Empty;
            while (v_InvalidLicenseNumber)
            {
                licenseNumber = getStringInput(k_LicenseNumberQuestion);
                s_GarageManager.IsLicenseNumberExist(licenseNumber, out bool isLicenseNumberExist);
                if (isLicenseNumberExist)
                {
                    break;
                }

                Console.Clear();
                Console.WriteLine($"The License Number You Entered ({licenseNumber}) Doesn't Exist. Please Try Again.{Environment.NewLine}");
            }

            return licenseNumber;
        }

        private List<string> displayLicensesInGarage()
        {
            List<string> licenseNumbersResult = new List<string>();
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Please select the status you want to filter by:{Environment.NewLine}");
            int index = 1;
            foreach (string status in Enum.GetNames(typeof(eVehicleStatus)))
            {
                message.AppendLine($"{index++}. {status}");
            }

            message.AppendLine($"{index}. Dont filter by status");
            GarageManager.GetEnumMinMax<eVehicleStatus>(out int minEnumValue, out int maxEnumValue);
            int selectedFilter = getIntInput(message, minEnumValue, maxEnumValue + 1);
            if (selectedFilter != maxEnumValue + 1)
            {
                licenseNumbersResult = s_GarageManager.GetGarageLicensesNumbersByFilter((eVehicleStatus)selectedFilter);
            }
            else
            {
                licenseNumbersResult = s_GarageManager.GetGarageLicensesNumbersByFilter();
            }

            Console.Clear();
            if (licenseNumbersResult.Count > 0)
            {
                Console.WriteLine("Licenses Numbers In Garage:");
                foreach (string licenseNumber in licenseNumbersResult)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
            else
            {
                Console.WriteLine("There are No Vehicles In The Garage That Match Your Filter");
            }

            return licenseNumbersResult;
        }

        private void insertNewVehicle()
        {
            Console.Clear();
            const string k_LicenseNumberQuestion = "Please enter the vehicle license number:";
            string licenseNumber = getStringInput(k_LicenseNumberQuestion);
            s_GarageManager.IsLicenseNumberExist(licenseNumber, out bool isLicenseNumberExist);

            if (isLicenseNumberExist)
            {
                s_GarageManager.ChangeStatusOfVehicle(licenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine("The vehicle is already in the garage. Your vehicle status has been updated to \"In Repair\"");
            }
            else
            {
                s_VehicleCardModel.LicenseNumber = licenseNumber;
                Console.Clear();
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
            bool isAllInputsValid = false;
            while (!isAllInputsValid)
            {
                List<string> propertiesData = new List<string>();
                try
                {
                    foreach (string message in messages)
                    {
                        Console.Write(message);
                        propertiesData.Add(Console.ReadLine());
                        Console.WriteLine();
                    }

                    s_VehicleCardModel.Vehicle.SetUniquePropertiesData(propertiesData);
                    isAllInputsValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
            }

            Console.Clear();
        }

        private void getWheelsAirPressure()
        {
            bool v_InvalidAirPressure = true;
            Console.WriteLine("Please enter current air pressure for all wheels:");
            while (v_InvalidAirPressure)
            {
                bool isFloat = float.TryParse(Console.ReadLine(), out float airPressure);
                if (isFloat)
                {
                    try
                    {
                        s_GarageManager.SetAirPressureForAllWheels(s_VehicleCardModel.Vehicle.Wheels, airPressure);
                        break;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine(ex.InnerException.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error! please enter a valid number");
                }
            }

            Console.Clear();
        }

        private void getCurrentEnergyStatus()
        {
            bool v_InvalidEnergyStatus = true;
            Console.WriteLine("Please enter vehicle's current energy:");
            while (v_InvalidEnergyStatus)
            {
                bool isFloat = float.TryParse(Console.ReadLine(), out float currentEnergy);
                if (isFloat)
                {
                    try
                    {
                        s_GarageManager.SetCurrentEnergyStatus(s_VehicleCardModel.Vehicle, currentEnergy);
                        break;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine($"Please enter a valid number from {ex.MinValue} to {ex.MaxValue}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine(ex.InnerException.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error! please enter a valid number");
                }
            }

            Console.Clear();
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
            Console.Clear();
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

                Console.WriteLine($"Please enter a valid integer from {i_MinValue} to {i_MaxValue}");
            }

            return selectedValue;
        }

        private float getFloatInput(StringBuilder i_Message)
        {
            float selectedValue = 0f;
            bool v_IsNotValid = true;
            Console.WriteLine(i_Message);
            while (v_IsNotValid)
            {
                bool isFloat = float.TryParse(Console.ReadLine(), out selectedValue);
                if (isFloat)
                {
                    break;
                }

                Console.WriteLine($"Please enter a valid float number.");
            }

            return selectedValue;
        }
    }
}
