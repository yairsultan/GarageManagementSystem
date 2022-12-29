using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicenseNumber;
        private string m_ModelName;
        private List<Wheel> m_Wheels;
        private EnergySource m_EnergySource;

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
        }

        public Vehicle(List<Wheel> i_Wheels, EnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber)
        {
            m_Wheels = i_Wheels;
            m_EnergySource = i_EnergySource;
            m_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
        }

        public abstract List<string> GetUniquePropertiesMessage();

        public abstract void SetUniquePropertiesData(List<string> i_PropertiesData);
    }
}
