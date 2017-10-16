using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomBookRS
    {
       public string SessionId { get; set; }
       public string BookingId { get; set; }
       public Status Status { get; set; }
    }
}
