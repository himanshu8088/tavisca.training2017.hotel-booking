using BusinessLayer.Model;
using System;

namespace Tavisca.Training2017.HotelBooking.BusinessLayer
{
    public class HotelSearchRequest
    {
        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public PointOfSale PointOfSale { get; set; }
    }
}
