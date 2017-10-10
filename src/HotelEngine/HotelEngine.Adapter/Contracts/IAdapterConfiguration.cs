using HotelEngine.Adapter.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Contracts
{
    public interface IAdapterConfiguration
    {
        HotelsAvailConfig GetHotelsAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ);
        RoomsAvailConfig GetRoomsAvailConfig(int hotelId);
    }
}
