using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class ElectricBattery : EnergySource
    {
        public override void AddEnergy(float i_EnergyQuantity, eFuelType? i_FuelType = null)
        {
            if (i_EnergyQuantity > 0)
            {
                CurrentEnergy = CurrentEnergy + i_EnergyQuantity;
            }
            else
            {
                string errorMessage = $"Error! You Can't Add Negative Charging Minutes! The Service Was Not Completed.";
                throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, MaxEnergyCapacity - CurrentEnergy);
            }
        }

        public ElectricBattery(float i_MaxBatteryTimeInHours)
            : base(i_MaxBatteryTimeInHours)
        {
        }
    }
}
