using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (isNumber && engineVolumeNum > 0)
            {
                m_EngineVolume = engineVolumeNum;
            }
            else
            {
                // todo
            }
        }

        private void validateLicenseType(string i_LicenseType)
        {
            bool isNumber = int.TryParse(i_LicenseType, out int licenseType);
            int minEnumValue = Enum.GetValues(typeof(eMotorcycleLicenseType)).Cast<int>().Min();
            int maxEnumValue = Enum.GetValues(typeof(eMotorcycleLicenseType)).Cast<int>().Max();
            if (isNumber && licenseType >= minEnumValue && licenseType <= maxEnumValue)
            {
                m_LicenseType = (eMotorcycleLicenseType)licenseType;
            }
            else
            {
                // todo
            }
        }
    }
}
