using BusinessLayer.Configuration;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Contracts
{
    public interface IConnectorConfiguration
    {
        HotelsAvailConfig GetHotelsAvailConfig(DateTime checkIn, DateTime checkOut, string poi = "Pune", int passengerCount = 1, int noOfRooms = 1, float latitude = 27.173891f, float longitude = 78.042068f, int posId = 101);
    }    
}
