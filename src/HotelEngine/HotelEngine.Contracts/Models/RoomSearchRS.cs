using System;
using System.Collections.Generic;

namespace HotelEngine.Contracts.Models
{
    public class RoomSearchRS
    {
        public int HotelId { get; set; }
        public string SessionId { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Uri> Images { get; set; }        
    }
}
