using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomPriceSearchRQ : RoomSearchRQ
    {        
        public string RoomName { get; set; }
    }
}
