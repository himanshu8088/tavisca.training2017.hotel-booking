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
        private IHotelConnector _hotelConnector;

        public PriceSearch()
        {
            _hotelConnector = new HotelConnector();
        }

        public async Task<RoomPriceSearchRS> SearchAsync(RoomPriceSearchRQ priceRQ)
        {
            var priceSearchRS = await _hotelConnector.SearchPriceAsync(priceRQ);
            return priceSearchRS;
        }
    }
}
