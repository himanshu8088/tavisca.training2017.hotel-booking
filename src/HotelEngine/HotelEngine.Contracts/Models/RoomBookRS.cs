using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class RoomBookRS
    {
       public string SessionId { get; set; }
       public string ConfirmationNo { get; set; }
       public Status Status { get; set; }
    }
}
