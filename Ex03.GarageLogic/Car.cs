using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int minEnumValue = Enum.GetValues(typeof(eColor)).Cast<int>().Min();
            int maxEnumValue = Enum.GetValues(typeof(eColor)).Cast<int>().Max();
            if (isNumber && carColorNumber >= minEnumValue && carColorNumber <= maxEnumValue)
            {
                m_Color = (eColor)carColorNumber;
            }
            else
            {
                // todo
            }
        }

        private void validateDoorsCount(string i_DoorsCount)
        {
            bool isNumber = int.TryParse(i_DoorsCount, out int doorsCountNumber);
            int minEnumValue = Enum.GetValues(typeof(eDoorsCount)).Cast<int>().Min();
            int maxEnumValue = Enum.GetValues(typeof(eDoorsCount)).Cast<int>().Max();
            if (isNumber && doorsCountNumber >= minEnumValue && doorsCountNumber <= maxEnumValue)
            {
                m_NumberOfDoors = (eDoorsCount)doorsCountNumber;
            }
            else
            {
                // todo
            }
        }
    }
}
