using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tavisca.Training2017.HotelBooking.BusinessLayer;

namespace Connector.Contracts
{
    public interface IHotelConnector
    {
        Task<List<HotelItinerary>> SearchHotelsAsync(HotelSearchRequest hotelSearchRequest);
    }
}
