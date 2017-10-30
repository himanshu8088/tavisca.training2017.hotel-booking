using HotelEngine.Contracts;
using System;
using HotelEngine.Contracts.Models;
using System.Threading.Tasks;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Adapter;

namespace HotelEngine.Core.Implementation
{
    public class PriceSearch : IPriceSearch
    {       
        private IHotelAdapter _hotelAdapter;

        public PriceSearch(IHotelAdapter hotelAdapter)
        {
            _hotelAdapter = hotelAdapter;
        }

        public async Task<RoomPriceSearchRS> SearchAsync(RoomPriceSearchRQ priceRQ)
        {
            var priceSearchRS = await _hotelAdapter.SearchRoomPriceAsync(priceRQ);
            return priceSearchRS;
        }
    }
}
