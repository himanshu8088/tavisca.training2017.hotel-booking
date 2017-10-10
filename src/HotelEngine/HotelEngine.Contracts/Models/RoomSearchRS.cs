using System;
using System.Collections.Generic;

namespace HotelEngine.Contracts.Models
{
    public class RoomSearchRS
    {
        public string SessionId { get; set; }
        public List<Room> Rooms { get; set; }       
    }
}
