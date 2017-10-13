using System;

namespace HotelEngine.Contracts.Models
{
    public class RoomPriceSearchRS
    {
        public string SessionId { get; set; }
        public int HotelId { get; set; }        
        public Fare ChargebleFare { get; set; }        
    }
}