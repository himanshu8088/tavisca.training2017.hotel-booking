
using Services.Contracts;
using Services.Model;
using System;
using System.Threading.Tasks;
using Tavisca.Training2017.HotelBooking.BusinessLayer;
using Tavisca.Training2017.HotelBooking.BusinessLayer.Contracts;
using Tavisca.Training2017.HotelBooking.Factories;

namespace ContractImplementation
{
    public class HotelService : IHotelService
    {
        public async Task<HotelSearchResult> SearchHotelsAsync(Services.Model.HotelSearchRequest hotelSearchRequest)
        {
            // Validation

            IHotelConnector hotelConnector = Factory.Get<IHotelConnector>();

            var request = new Tavisca.Training2017.HotelBooking.BusinessLayer.HotelSearchRequest()
            {
                SearchText = hotelSearchRequest.SearchText,
                CheckInDate = hotelSearchRequest.CheckInDate,
                CheckoutDate = hotelSearchRequest.CheckoutDate
            };

            request.PointOfSale = Factory.Get<IConfigurationService>().GetPointofSale(hotelSearchRequest.PosId);
            
            var result = await hotelConnector.SearchHotelsAsync(request);

            HotelSearchResult hotelSearchResult = new HotelSearchResult();
            hotelSearchResult.Hotels = new System.Collections.Generic.List<Hotel>();
            foreach (var item in result)
            {
                hotelSearchResult.Hotels.Add(new Hotel()
                {
                    Name = item.Name
                });
            }

            return hotelSearchResult;
        }
    }
}
