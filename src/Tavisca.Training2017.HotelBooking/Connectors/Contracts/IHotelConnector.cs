using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connector.Contracts
{
    public interface IHotelConnector
    {
        Task<Connector.Model.HotelIteneraryRS> SearchHotelsAsync(Connector.Model.HotelIteneraryRQ hotelSearchRQ);
    }
}
