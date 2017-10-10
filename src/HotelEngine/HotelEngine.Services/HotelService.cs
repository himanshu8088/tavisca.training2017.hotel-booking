using System.Threading.Tasks;
using System;
using HotelEngine.Contracts.Models;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Core.Implementation;
using HotelEngine.Core.Factories;

namespace HotelEngine.Services
{    
    public class HotelService : IHotelService
    {
        private IHotelSearch _hotelSearch;
        private IRoomSearch _roomSearch;

        public HotelService()
        {
            _hotelSearch = Factory.Get<IHotelSearch>() as IHotelSearch;
            _roomSearch = Factory.Get<IRoomSearch>() as IRoomSearch;
        }

        public async Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest)
        {
            hotelSearchRequest.SessionId = Guid.NewGuid();
            var hotelSearchRS = await _hotelSearch.SearchAsync(hotelSearchRequest);
            return hotelSearchRS;
        }

        public async Task<RoomSearchRS> RoomSearchAsync(RoomSearchRQ roomSearchRequest)
        {            
            var roomSearchRS = await _roomSearch.SearchAsync(roomSearchRequest);
            return roomSearchRS;
        }        
    }
}

