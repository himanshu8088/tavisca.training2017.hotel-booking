using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomSearchRQ : HotelSearchRQ
    {
        public string HotelId { get; set; }
    }
}
