using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelTank : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        internal void ChangeAmountOfFuel(float i_AmountOfFuelInLiters, eFuelType i_FuelType)
        {
            if (i_AmountOfFuelInLiters > 0 && i_FuelType == r_FuelType && i_AmountOfFuelInLiters + CurrentEnergy <= MaxEnergyCapacity)
            {
                CurrentEnergy = CurrentEnergy + i_AmountOfFuelInLiters;
            }
        }

        public FuelTank(eFuelType i_FuelType, float i_MaxAmountFuelInLiters)
            : base(i_MaxAmountFuelInLiters)
        {
            r_FuelType = i_FuelType;
        }
    }
}
