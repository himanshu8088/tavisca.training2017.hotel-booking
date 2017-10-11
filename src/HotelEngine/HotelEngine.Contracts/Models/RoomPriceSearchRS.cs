using System;

namespace HotelEngine.Contracts.Models
{
    public class RoomPriceSearchRS
    {
        public string SessionId { get; set; }
        public Fare Fare { get; set; }
    }
}