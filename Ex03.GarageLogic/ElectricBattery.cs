using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class ElectricBattery : EnergySource
    {
        public ElectricBattery(float i_MaxBatteryTimeInHours)
            : base(i_MaxBatteryTimeInHours)
        {
        }

        public override void AddEnergy(float i_HoursToCharge, eFuelType? i_FuelType = null)
        {
            if (i_HoursToCharge >= 0)
            {
                CurrentEnergy += i_HoursToCharge;
            }
            else
            {
                string errorMessage = $"Error! You Can't Add Negative Charging Minutes! The Service Was Not Completed.";
                throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, MaxEnergyCapacity - CurrentEnergy);
            }
        }
    }
}
