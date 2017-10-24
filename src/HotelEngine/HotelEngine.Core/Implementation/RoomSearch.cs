using HotelEngine.Adapter;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System.Threading.Tasks;

namespace HotelEngine.Core.Implementation
{
    public class RoomSearch : IRoomSearch
    {
        private IHotelAdapter _hotelConnector;

        public RoomSearch()
        {
            _hotelConnector = new HotelAdapter();
        }

        public async Task<RoomSearchRS> SearchAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomSearchRS = await _hotelConnector.SearchRoomsAsync(roomSearchRQ);
            return roomSearchRS;
        }
    }
}
