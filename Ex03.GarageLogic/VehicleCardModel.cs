namespace Ex03.GarageLogic
{
    public class VehicleCardModel
    {
        private Vehicle m_vehicle;
        private eVehicleType m_VehicleType;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private string m_ModelName;
        private string m_LicenseNumber;
        private string m_WheelsProducerName;

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_vehicle; }
            set { m_vehicle = value; }
        }

        public string WheelProducerName
        {
            get { return m_WheelsProducerName; }
            set { m_WheelsProducerName = value; }
        }

        public eVehicleType VehicleType
        {
            get { return m_VehicleType; }
            set { m_VehicleType = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }
    }
}
