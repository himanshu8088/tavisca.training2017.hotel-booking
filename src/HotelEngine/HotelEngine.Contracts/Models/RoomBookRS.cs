using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomBookRS:RoomPriceSearchRQ
    {
        UserDetail GuestDetail { get; set; }
        CardDetail CardDetail { get; set; }
    }
}
