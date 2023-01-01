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
        private readonly string r_ModelName;
        private readonly EnergySource r_EnergySource;
        private readonly List<Wheel> r_Wheels;

        public List<Wheel> Wheels
        {
            get { return r_Wheels; }
        }

        public EnergySource EnergySource
        {
            get { return r_EnergySource; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
        }

        public Vehicle(List<Wheel> i_Wheels, EnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber)
        {
            r_Wheels = i_Wheels;
            r_EnergySource = i_EnergySource;
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
        }

        public abstract List<string> GetUniquePropertiesMessage();

        public abstract void SetUniquePropertiesData(List<string> i_PropertiesData);
    }
}
