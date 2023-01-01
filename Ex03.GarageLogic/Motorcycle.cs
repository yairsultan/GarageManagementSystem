using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseType
        {
            A = 1,
            A1,
            AA,
            B,
        }

        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineVolume;

        public eMotorcycleLicenseType LicenseType
        {
            get { return m_LicenseType; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
        }

        public Motorcycle(List<Wheel> i_Wheels, EnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber)
            : base(i_Wheels, i_EnergySource, i_ModelName, i_LicenseNumber)
        {
        }

        public override List<string> GetUniquePropertiesMessage()
        {
            List<string> messagesList = new List<string>();
            StringBuilder message = new StringBuilder();
            message.AppendLine("Please select license type:");
            message.AppendLine("1. A");
            message.AppendLine("2. A1");
            message.AppendLine("3. AA");
            message.AppendLine("4. B" + Environment.NewLine);
            messagesList.Add(message.ToString());
            message.Clear();
            message.AppendLine("Plesae select engine volume:");
            messagesList.Add(message.ToString());

            return messagesList;
        }

        public override void SetUniquePropertiesData(List<string> i_PropertiesData)
        {
            validateLicenseType(i_PropertiesData[0]);
            validateEngineVolume(i_PropertiesData[1]);
        }

        private void validateEngineVolume(string i_EngineVolume)
        {
            bool isNumber = int.TryParse(i_EngineVolume, out int engineVolumeNum);
            if (isNumber)
            {
                if (engineVolumeNum > 0)
                {
                    m_EngineVolume = engineVolumeNum;
                }
                else
                {
                    string errorMessage = $"Engine Volume Error! You Can Only Slect a Value grater then 0! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, int.MaxValue);
                }
            }
            else
            {
                string errorMessage = $"Engine Volume Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        private void validateLicenseType(string i_LicenseType)
        {
            bool isNumber = int.TryParse(i_LicenseType, out int licenseType);
            GarageManager.GetEnumMinMax<eMotorcycleLicenseType>(out int minEnumValue, out int maxEnumValue);
            if (isNumber)
            {
                if (licenseType >= minEnumValue && licenseType <= maxEnumValue)
                {
                    m_LicenseType = (eMotorcycleLicenseType)licenseType;
                }
                else
                {
                    string errorMessage = $"License Type Error! You Can Only Slect a Value between {minEnumValue} and {maxEnumValue}! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), minEnumValue, maxEnumValue);
                }
            }
            else
            {
                string errorMessage = $"License Type Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        public override string ToString()
        {
            string resultMessage = string.Format(
                "Motorcycle Has Engine Volume of {1}.{0}Motorcycle Has License Type of {2}.{0}",
                Environment.NewLine,
                m_EngineVolume,
                m_LicenseType);

            return resultMessage;
        }
    }
}
