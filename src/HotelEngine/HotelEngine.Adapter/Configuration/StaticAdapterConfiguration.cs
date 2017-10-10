using System;
using Proxies;
using HotelEngine.Adapter.Contracts;

namespace HotelEngine.Adapter.Configuration
{
    public class StaticAdapterConfiguration : IAdapterConfiguration
    {       
        public HotelsAvailConfig GetHotelsAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            return new HotelsAvailConfig(hotelSearchRQ);
        }
        public RoomsAvailConfig GetRoomsAvailConfig(int hotelId)
        {
            return new RoomsAvailConfig(hotelId);
        }
    }
}
