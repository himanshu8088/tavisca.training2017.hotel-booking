using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tavisca.Training2017.HotelBooking.Models;
using Engines;

namespace Tavisca.Training2017.HotelBooking.Factories
{
    public class SearchRQFactory
    {
        public HotelSearchRQ CreateCompleteRQ(SearchRQ searchRQ)
        {
            HotelSearchRQ request = new HotelSearchRQ();
            var hotelSearchCriterion = CreateHotelSearchCriterion(101);
            request.SessionId = Guid.NewGuid().ToString();
            request.HotelSearchCriterion = hotelSearchCriterion;
            return request;
        }

       
        private HotelSearchCriterion CreateHotelSearchCriterion(int posId)
        {
            var hotelSearchCriterion = new HotelSearchCriterion();
            hotelSearchCriterion.Pos = CreatePOS(posId);
            return hotelSearchCriterion;
        }

        private PointOfSale CreatePOS(int id)
        {
            var pos = new PointOfSale();
            pos.PosId = id;
            return pos;
        }

    }
}

