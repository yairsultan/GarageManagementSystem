using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class VehicleCard
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        public VehicleCard()
        {
        }

        public VehicleCard(VehicleCardModel i_Vehicle)
        {
            m_OwnerName = i_Vehicle.OwnerName;
            m_OwnerPhoneNumber = i_Vehicle.OwnerPhoneNumber;
            m_VehicleStatus = i_Vehicle.VehicleStatus;
            m_Vehicle = i_Vehicle.Vehicle;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhoneNumber; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }
    }
}
