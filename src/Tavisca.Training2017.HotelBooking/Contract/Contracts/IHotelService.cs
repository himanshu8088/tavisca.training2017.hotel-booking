using Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IHotelService
    {
        Task<List<Hotel>> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest);
    }
}
