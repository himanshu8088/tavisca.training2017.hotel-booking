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
        private IHotelAdapter _hotelConnector;

        public PriceSearch()
        {
            _hotelConnector = new HotelAdapter();
        }

        public async Task<RoomPriceSearchRS> SearchAsync(RoomPriceSearchRQ priceRQ)
        {
            var priceSearchRS = await _hotelConnector.SearchPriceAsync(priceRQ);
            return priceSearchRS;
        }
    }
}
