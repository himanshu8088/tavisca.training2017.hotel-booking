using Connector.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tavisca.Training2017.HotelBooking.BusinessLayer;


namespace Connector
{
    public class HotelConnector : IHotelConnector
    {
        Task<List<HotelItinerary>> IHotelConnector.SearchHotelsAsync(HotelSearchRequest hotelSearchRequest)
        {
            HotelEngineClient client = new HotelEngineClient();

            HotelSearchRQ hotelSearchRQ = new HotelSearchRQ();

            Task<HotelSearchRS> hotelSearchRS;

            // Translate

            try
            {
                hotelSearchRS = client.HotelAvailAsync(hotelSearchRQ);

                // Translate
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {


            }
            throw new NotImplementedException();
        }

    }
}
