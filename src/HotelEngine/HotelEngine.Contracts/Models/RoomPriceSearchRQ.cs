using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomPriceSearchRQ : RoomSearchRQ
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
    }
}
