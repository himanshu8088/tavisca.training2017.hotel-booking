using HotelEngine.Contracts.Models;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IHotelAdapter
    {        
        Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRQ);
        Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ);
        Task<RoomPriceSearchRS> SearchRoomPriceAsync(RoomPriceSearchRQ priceRQ);
        Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ);
    }
}
