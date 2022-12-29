using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelTank;

namespace Ex03.GarageLogic
{
    public class ElectricBattery : EnergySource
    {
        private void batteryCharging(float i_NumberOfHour)
        {
            if(i_NumberOfHour < 0 && CurrentEnergy + i_NumberOfHour <= MaxEnergyCapacity)
            {
                CurrentEnergy += i_NumberOfHour;
            }
        }

        public ElectricBattery(float i_MaxBatteryTimeInHours)
            : base(i_MaxBatteryTimeInHours)
        {
        }
    }
}
