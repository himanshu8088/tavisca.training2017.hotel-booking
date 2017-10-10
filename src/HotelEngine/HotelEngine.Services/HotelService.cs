using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using HotelEngine.Contracts.Models;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Services.Validator;
using HotelEngine.Core.Implementation;

namespace HotelEngine.Services
{    
    public class HotelService : IHotelService
    {
        private IHotelSearch _hotelSearch;
        private IRoomSearch _roomSearch;

        public HotelService()
        {
            _hotelSearch = new HotelSearch();
        }

        public async Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest)
        {
            hotelSearchRequest.SessionId = Guid.NewGuid();
            var hotelSearchRS = await _hotelSearch.SearchAsync(hotelSearchRequest);
            return hotelSearchRS;
        }

        public async Task<List<Room>> RoomSearchAsync(RoomSearchRQ roomSearchRequest)
        {
            roomSearchRequest.SessionId = Guid.NewGuid();
            var roomSearchRS = await _roomSearch.SearchAsync(roomSearchRequest);
            return roomSearchRS;
        }        
    }
}

