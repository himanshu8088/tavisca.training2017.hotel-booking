using System.Collections.Generic;

namespace HotelEngine.Contracts.Models
{
    public class HotelSearchRS
    {
        public string SessionId { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
