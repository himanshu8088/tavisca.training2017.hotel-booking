using System;
using BusinessLayer.Contracts;
using BusinessLayer.Configuration;

namespace BusinessLayer.Configuration
{
    public class StaticConnectorConfiguration : IConnectorConfiguration
    {             
        public HotelsAvailConfig GetHotelsAvailConfig( DateTime checkIn, DateTime checkOut, string poi = "Pune", int passengerCount = 1, int noOfRooms = 1, float latitude = 27.173891f, float longitude = 78.042068f, int posId = 101)
        {
            return new HotelsAvailConfig(poi,checkIn,checkOut,passengerCount = 1,noOfRooms = 1,latitude = 27.173891f,longitude = 78.042068f,posId = 101);
        }

        public RoomsAvailConfig GetRoomsAvailConfig()
        {
            return new RoomsAvailConfig();
        }
       
    }
}
