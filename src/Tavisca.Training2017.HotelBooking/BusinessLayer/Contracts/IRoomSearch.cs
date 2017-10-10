using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IRoomSearch
    {
        Task<Model.HotelItinerary> SearchAsync(Model.RoomSearchRQ hotelSearchRequest);
    }
}
