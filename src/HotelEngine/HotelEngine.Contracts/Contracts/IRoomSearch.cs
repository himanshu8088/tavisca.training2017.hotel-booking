using HotelEngine.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IRoomSearch
    {
        Task<List<Room>> SearchAsync(RoomSearchRQ hotelSearchRequest);
    }
}
