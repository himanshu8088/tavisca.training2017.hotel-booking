using Services.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IHotelService
    {
        Task<HotelSearchResult> SearchHotelsAsync(HotelSearchRequest hotelSearchRequest);
    }
}
