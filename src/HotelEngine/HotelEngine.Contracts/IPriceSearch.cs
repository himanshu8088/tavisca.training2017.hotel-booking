using HotelEngine.Contracts.Models;
using System.Threading.Tasks;

namespace HotelEngine.Contracts
{
    public interface IPriceSearch
    {
        Task<RoomPriceSearchRS> SearchAsync(RoomPriceSearchRQ priceRQ);
    }
}
