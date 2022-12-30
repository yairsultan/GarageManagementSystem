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
        public override void AddEnergy(float i_EnergyQuantity, eFuelType? i_FuelType = null)
        {
            CurrentEnergy = CurrentEnergy;
        }

        public ElectricBattery(float i_MaxBatteryTimeInHours)
            : base(i_MaxBatteryTimeInHours)
        {
        }
    }
}
