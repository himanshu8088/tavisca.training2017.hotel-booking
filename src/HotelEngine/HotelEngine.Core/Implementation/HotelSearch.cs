using System.Threading.Tasks;
using System;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Adapter;

namespace HotelEngine.Core.Implementation
{
    public class HotelSearch : IHotelSearch
    {
        private IHotelConnector _hotelConnector;

        public HotelSearch()
        {
            _hotelConnector = new HotelConnector();
        }

        public async Task<HotelSearchRS> SearchAsync(HotelSearchRQ hotelSearchRequest)
        {
            var hotelSearchRS = await _hotelConnector.SearchHotelsAsync(hotelSearchRequest);
            return hotelSearchRS;
        }
       
        public async Task<RoomSearchRS> RoomSearchAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomSearchRS = await _hotelConnector.SearchRoomsAsync(roomSearchRQ);
            return roomSearchRS;
        }
    }
}

