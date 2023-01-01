using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            Blue,
            White,
            Gray,
        }

        public enum eDoorsCount
        {
            two = 2,
            Three,
            Four,
            five,
        }

        private eColor m_Color;
        private eDoorsCount m_NumberOfDoors;

        public eColor Color
        {
            get { return m_Color; }
        }

        public eDoorsCount DoorsCount
        {
            get { return m_NumberOfDoors; }
        }

        public Car(string i_LicenseNumber)
            : base(i_LicenseNumber)
        {
        }

        public Car(List<Wheel> i_Wheels, EnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber)
            : base(i_Wheels, i_EnergySource, i_ModelName, i_LicenseNumber)
        {
        }

        public override List<string> GetUniquePropertiesMessage()
        {
            List<string> messagesList = new List<string>();
            StringBuilder message = new StringBuilder();
            message.AppendLine("Please select car color:");
            message.AppendLine("1. Red");
            message.AppendLine("2. Blue");
            message.AppendLine("3. White");
            message.AppendLine($"4. Gray{Environment.NewLine}");
            messagesList.Add(message.ToString());
            message.Clear();
            message.AppendLine("Please select doors count:");
            message.AppendLine("2. two");
            message.AppendLine("3. Three");
            message.AppendLine("4. Four");
            message.AppendLine($"5. five{Environment.NewLine}");
            messagesList.Add(message.ToString());

            return messagesList;
        }

        public override void SetUniquePropertiesData(List<string> i_PropertiesData)
        {
            validateColor(i_PropertiesData[0]);
            validateDoorsCount(i_PropertiesData[1]);
        }

        private void validateColor(string i_CarColor)
        {
            bool isNumber = int.TryParse(i_CarColor, out int carColorNumber);
            GarageManager.GetEnumMinMax<eColor>(out int minEnumValue, out int maxEnumValue);
            if (isNumber)
            {
                if (carColorNumber >= minEnumValue && carColorNumber <= maxEnumValue)
                {
                    m_Color = (eColor)carColorNumber;
                }
                else
                {
                    string errorMessage = $"Car Color Error! You Can Only Slect a Value between {minEnumValue} and {maxEnumValue}! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), minEnumValue, maxEnumValue);
                }
            }
            else
            {
                string errorMessage = $"Car Color Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        private void validateDoorsCount(string i_DoorsCount)
        {
            bool isNumber = int.TryParse(i_DoorsCount, out int doorsCountNumber);
            GarageManager.GetEnumMinMax<eDoorsCount>(out int minEnumValue, out int maxEnumValue);
            if (isNumber)
            {
                if (doorsCountNumber >= minEnumValue && doorsCountNumber <= maxEnumValue)
                {
                    m_NumberOfDoors = (eDoorsCount)doorsCountNumber;
                }
                else
                {
                    string errorMessage = $"Doors Count Error! You Can Only Slect a Value between {minEnumValue} and {maxEnumValue}! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), minEnumValue, maxEnumValue);
                }
            }
            else
            {
                string errorMessage = $"Doors Count Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        public override string ToString()
        {
            string resultMessage = string.Format(
                "Car has {1} doors.{0}Car is {2}.{0}",
                Environment.NewLine,
                m_NumberOfDoors,
                m_Color);

            return resultMessage;
        }
    }
}
