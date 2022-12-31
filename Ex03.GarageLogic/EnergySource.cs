using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private readonly float r_MaxEnergyCapacity;
        private float m_CurrentEnergy;

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }

            set
            {
                if (value >= 0 && value + m_CurrentEnergy <= r_MaxEnergyCapacity)
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    string errorMessage = $"Error! You Can Only Add A Fuel Amount Between 0 and {r_MaxEnergyCapacity - m_CurrentEnergy}! Please Try Again.";
                    throw new ValueOutOfRangeException(errorMessage, new Exception(), 0, r_MaxEnergyCapacity - m_CurrentEnergy);
                }
            }
        }

        public float MaxEnergyCapacity
        {
            get { return r_MaxEnergyCapacity; }
        }

        public EnergySource(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public abstract void AddEnergy(float i_EnergyQuantity, eFuelType? i_FuelType = null);
    }
}
