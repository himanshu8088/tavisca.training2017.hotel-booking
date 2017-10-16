using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomBookRQ:RoomPriceSearchRQ
    {
       public UserDetail GuestDetail { get; set; }
       public CardDetail CardDetail { get; set; }
    }
}
