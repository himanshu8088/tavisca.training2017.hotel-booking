
using HotelEngine.Adapter;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace HotelEngine.Core.Implementation
{
    public class RoomSearch : IRoomSearch
    {
        private IHotelConnector _hotelConnector;

        public RoomSearch()
        {
            _hotelConnector = new HotelConnector();
        }

        public async Task<RoomSearchRS> SearchAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomSearchRS = await _hotelConnector.SearchRoomsAsync(roomSearchRQ);
            return roomSearchRS;
        }
    }
}
