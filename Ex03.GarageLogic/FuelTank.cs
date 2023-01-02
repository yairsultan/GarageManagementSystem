using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class FuelTank : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public FuelTank(eFuelType i_FuelType, float i_MaxAmountFuelInLiters)
            : base(i_MaxAmountFuelInLiters)
        {
            r_FuelType = i_FuelType;
        }

        public override void AddEnergy(float i_EnergyQuantity, eFuelType? i_FuelType = null)
        {
            if(r_FuelType != i_FuelType)
            {
                throw new ArgumentException($"Wrong energy type. please enter the correct type: {r_FuelType.ToString()}.");
            }

            if (i_EnergyQuantity > 0)
            {
                CurrentEnergy += i_EnergyQuantity;
            }
            else
            {
                string errorMessage = $"Error! You Can't Add Negative Amount Of Fuel! The Service Was Not Completed.";
                throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, MaxEnergyCapacity - CurrentEnergy);
            }
        }
    }
}
