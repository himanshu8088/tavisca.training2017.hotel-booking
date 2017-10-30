using System.Threading.Tasks;
using System;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Adapter;

namespace HotelEngine.Core.Implementation
{
    public class HotelSearch : IHotelSearch
    {
        private IHotelAdapter _hotelAdapter;

        public HotelSearch(IHotelAdapter hotelAdapter)
        {
            _hotelAdapter = hotelAdapter;
        }

        public async Task<HotelSearchRS> SearchAsync(HotelSearchRQ hotelSearchRequest)
        {
            var hotelSearchRS = await _hotelAdapter.SearchHotelsAsync(hotelSearchRequest);
            return hotelSearchRS;
        }
            
    }
}

