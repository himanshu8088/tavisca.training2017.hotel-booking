using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Model;
using System.Threading.Tasks;
using Connector.Contracts;
using BusinessLayer.Factories;
using BusinessLayer.Configuration;

namespace BusinessLayer.ContractsImplementation
{
    public class RoomSearch : IRoomSearch
    {
        public Task<HotelItinerary> SearchAsync(RoomSearchRQ roomSearchRequest)
        {
            IHotelConnector hotelConnector = Factory.Get<IHotelConnector>() as IHotelConnector;
            StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration();
            RoomsAvailConfig roomAvailConfig = configurationService.GetRoomsAvailConfig();//roomSearchRequest.CheckinDate, roomSearchRequest.CheckoutDate, roomSearchRequest.SearchText, roomSearchRequest.PsgCount, roomSearchRequest.NoOfRooms, roomSearchRequest.Location.Latitude, roomSearchRequest.Location.Longitude, roomSearchRequest.HotelId
            Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
            {
                HotelSearchCriterion = roomAvailConfig.HotelSearchCriterion,
                ResultRequested = roomAvailConfig.ResultRequested,
                SessionId = roomSearchRequest.SessionId.ToString(),
                HotelId = roomSearchRequest.HotelId
            };

            Task<Connector.Model.HotelIteneraryRS> hotelSearchRS = hotelConnector.SearchHotelsAsync(hotelSearchRQ);
            throw new NotImplementedException();
        }
    }
}
