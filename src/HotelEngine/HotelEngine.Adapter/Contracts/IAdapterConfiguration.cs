using HotelEngine.Adapter.Configuration;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Contracts
{
    public interface IAdapterConfiguration
    {
        HotelsAvailConfig GetHotelsAvailConfig(HotelSearchRQ hotelSearchRQ);
        RoomsAvailConfig GetRoomsAvailConfig(RoomSearchRQ roomSearchRQ);
    }
}
