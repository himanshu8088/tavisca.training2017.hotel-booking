using HotelEngine.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IHotelService
    {
        Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest);
        Task<RoomSearchRS> RoomSearchAsync(RoomSearchRQ roomSearchRequest);
        Task<RoomPriceSearchRS> RoomPriceSearchAsync(RoomPriceSearchRQ priceRQ);
        Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ);
    }
}
