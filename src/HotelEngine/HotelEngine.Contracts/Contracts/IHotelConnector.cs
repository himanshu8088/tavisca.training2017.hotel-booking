using HotelEngine.Contracts.Models;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IHotelConnector
    {
        Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRQ);
        Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ);
    }
}
