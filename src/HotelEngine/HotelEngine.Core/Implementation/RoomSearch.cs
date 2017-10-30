using HotelEngine.Adapter;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System.Threading.Tasks;

namespace HotelEngine.Core.Implementation
{
    public class RoomSearch : IRoomSearch
    {
        private IHotelAdapter _hotelAdapter;

        public RoomSearch(IHotelAdapter hotelAdapter)
        {
            _hotelAdapter = hotelAdapter;
        }

        public async Task<RoomSearchRS> SearchAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomSearchRS = await _hotelAdapter.SearchRoomsAsync(roomSearchRQ);
            return roomSearchRS;
        }
    }
}
