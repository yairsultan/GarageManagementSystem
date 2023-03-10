using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Exceptions;

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
            message.AppendLine("2. False");
            messagesList.Add(message.ToString());
            message.Clear();
            message.AppendLine($"Plesae select maximum volume charge:");
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
            bool isNumber = int.TryParse(i_HazardousMaterials, out int isHazardousMaterials);
            if (isNumber)
            {
                if (isHazardousMaterials >= 1 && isHazardousMaterials <= 2)
                {
                    m_IsTransportingHazardousMaterials = isHazardousMaterials == 1;
                }
                else
                {
                    string errorMessage = $"Hazardous Materials Error! You Can Only Slect a Value Between 1 and 2! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), 1, 2);
                }
            }
            else
            {
                string errorMessage = $"Hazardous Materials Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        private void validateVolumeCharge(string i_VolumeCharge)
        {
            bool isFloat = float.TryParse(i_VolumeCharge, out float volumeChargeFloat);
            if (isFloat)
            {
                if (volumeChargeFloat > 0)
                {
                    m_MaximumVolumeCharge = volumeChargeFloat;
                }
                else
                {
                    string errorMessage = $"Volume Charge Error! You Can Only Slect a Value Grater then 0! Please Try Again.{Environment.NewLine}";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, float.MaxValue);
                }
            }
            else
            {
                string errorMessage = $"Volume Charge Error! Your input is not in the correct format! Please Try Again.{Environment.NewLine}";
                throw new FormatException(errorMessage);
            }
        }

        public override string ToString()
        {
            string isMovingRefrigeratedCargo = m_IsTransportingHazardousMaterials ? "is" : "is not";
            string resultMessage = string.Format(
                "Truck {1} Transporting Hazardous Materials.{0}Truck Maximum Volume Charge is: {2}.{0}",
                Environment.NewLine,
                isMovingRefrigeratedCargo,
                m_MaximumVolumeCharge);

            return resultMessage;
        }
    }
}
