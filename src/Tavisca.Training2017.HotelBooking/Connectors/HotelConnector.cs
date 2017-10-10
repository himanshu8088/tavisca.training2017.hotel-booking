using Connector.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Connector.Model;

namespace Connector
{
    public class HotelConnector : IHotelConnector
    {
        public async Task<Connector.Model.HotelIteneraryRS> SearchHotelsAsync(Connector.Model.HotelIteneraryRQ hotelSearchRQ)
        {
            HotelEngineClient client=null;
            Connector.Model.HotelIteneraryRS hotelSearchRS = new Connector.Model.HotelIteneraryRS();
            try
            {
                client = new HotelEngineClient();                  
                
                var hotelSearchRes = new HotelSearchRQ()
                {
                    HotelSearchCriterion = hotelSearchRQ.HotelSearchCriterion,
                    SessionId = hotelSearchRQ.SessionId,
                    Filters = hotelSearchRQ.Filters,
                    PagingInfo = hotelSearchRQ.PagingInfo,
                    ResultRequested = hotelSearchRQ.ResultRequested                    
                };
                Task<HotelSearchRS> response= client.HotelAvailAsync(hotelSearchRes);
                HotelSearchRS hotelSearchResult = response.GetAwaiter().GetResult();
                var itineraries = hotelSearchResult.Itineraries;                            
                hotelSearchRS.HotelItineraries = new List<HotelItinerary>(itineraries);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await client.CloseAsync();
            }
            return hotelSearchRS;
        }

        public async Task<Connector.Model.HotelIteneraryRS> SearchRoomsAsync(Connector.Model.HotelIteneraryRQ searchRQ)
        {
            var client = new HotelEngineClient();
            client.HotelRoomAvailAsync(GetRoomAvailRequest(searchRQ));
            return null;
        }

        private HotelRoomAvailRQ GetRoomAvailRequest(HotelIteneraryRQ searchRQ)
        {
            return new HotelRoomAvailRQ
            {
                HotelSearchCriterion = searchRQ.HotelSearchCriterion,
                Itinerary = new HotelItinerary { }
            };
        }
    }
}
