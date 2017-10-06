using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Model;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IHotelSearch
    {
        Task<List<BusinessLayer.Model.HotelItinerary>> SearchAsync(HotelSearchRQ hotelSearchRequest);
    }
}
