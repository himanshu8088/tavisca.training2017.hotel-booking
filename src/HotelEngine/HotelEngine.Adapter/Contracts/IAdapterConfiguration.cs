using BookingProxy;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Contracts.Models;
using Proxies;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelEngine.Adapter.Contracts
{
    public interface IAdapterConfiguration
    {
        HotelsAvailConfig GetMultiAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ);
        RoomsAvailConfig GetSingleAvailConfig(RoomSearchRQ roomSearchRQ);
        TripProductConfig GetTripProductConfig(RoomPriceSearchRQ roomPriceSearchRQ, Proxies.HotelRoomAvailRS hotelRoomAvailRS);
        CompleteBookConfig GetCompleteBookConfig(TripFolderBookRS tripFolderBookRS, Guid sessionId);
        TripFolderBookConfig GetTripFolderBookConfig(HotelTripProduct hotelTripProduct, RoomBookRQ roomBookRQ);
    }
}
