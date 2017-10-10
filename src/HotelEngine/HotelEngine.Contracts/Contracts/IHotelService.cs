using HotelEngine.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IHotelService
    {
        Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest);
        Task<Hotel> RoomSearchAsync(RoomSearchRQ roomSearchRequest);
    }
}
