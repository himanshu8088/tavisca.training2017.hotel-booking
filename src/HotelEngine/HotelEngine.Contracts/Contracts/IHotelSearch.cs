using System.Collections.Generic;
using System.Threading.Tasks;
using HotelEngine.Contracts.Models;

namespace HotelEngine.Contracts.Contracts
{
    public interface IHotelSearch
    {
        Task<HotelSearchRS> SearchAsync(HotelSearchRQ hotelSearchRequest);
    }
}
