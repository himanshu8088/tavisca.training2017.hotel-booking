using System.Threading.Tasks;
using System;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Adapter;

namespace HotelEngine.Core.Implementation
{
    public class HotelSearch : IHotelSearch
    {
        private IHotelAdapter _hotelConnector;

        public HotelSearch()
        {
            _hotelConnector = new HotelAdapter();
        }

        public async Task<HotelSearchRS> SearchAsync(HotelSearchRQ hotelSearchRequest)
        {
            var hotelSearchRS = await _hotelConnector.SearchHotelsAsync(hotelSearchRequest);
            return hotelSearchRS;
        }
            
    }
}

