using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTransportingHazardousMaterials;
        private float m_MaximumVolumeCharge;

        public bool IsTransportingHazardousMaterials
        {
            get { return m_IsTransportingHazardousMaterials; }
        }

        public float MaximumVolumeCharge
        {
            get { return m_MaximumVolumeCharge; }
        }

        public Truck(List<Wheel> i_Wheels, EnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber)
            : base(i_Wheels, i_EnergySource, i_ModelName, i_LicenseNumber)
        {
        }

        public override List<string> GetUniquePropertiesMessage()
        {
            List<string> messagesList = new List<string>();
            StringBuilder message = new StringBuilder();
            message.AppendLine("Please select is transporting hazardous materials:");
            message.AppendLine("1. True");
            message.AppendLine($"2. False{Environment.NewLine}");
            messagesList.Add(message.ToString());
            message.Clear();
            message.AppendLine($"Plesae select maximum volume charge:{Environment.NewLine}");
            messagesList.Add(message.ToString());

            return messagesList;
        }

        public override void SetUniquePropertiesData(List<string> i_PropertiesData)
        {
            validateHazardousMaterials(i_PropertiesData[0]);
            validateVolumeCharge(i_PropertiesData[1]);
        }

        private void validateHazardousMaterials(string i_HazardousMaterials)
        {
            bool isbool = bool.TryParse(i_HazardousMaterials, out bool isHazardousMaterials);
            if (isbool)
            {
                m_IsTransportingHazardousMaterials = isHazardousMaterials;
            }
            else
            {
                // todo
            }
        }

        private void validateVolumeCharge(string i_VolumeCharge)
        {
            bool isFloat = float.TryParse(i_VolumeCharge, out float volumeChargeFloat);
            if (isFloat && volumeChargeFloat > 0)
            {
                m_MaximumVolumeCharge = volumeChargeFloat;
            }
            else
            {
                // todo
            }
        }

        public override string ToString()
        {
            string resultMessage = string.Empty;
            string isMovingRefrigeratedCargo = m_IsTransportingHazardousMaterials ? "is" : "is not";

            resultMessage = string.Format(
                "Truck {1} Transporting Hazardous Materials.{0}Truck Maximum Volume Charge is: {2}.{0}",
                Environment.NewLine,
                isMovingRefrigeratedCargo,
                m_MaximumVolumeCharge);

            return resultMessage;
        }
    }
}
