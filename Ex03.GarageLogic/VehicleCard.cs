namespace Ex03.GarageLogic
{
    public class VehicleCard
    {
        private readonly Vehicle r_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public VehicleCard(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_Vehicle = i_Vehicle;
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return r_OwnerPhoneNumber; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }
    }
}
