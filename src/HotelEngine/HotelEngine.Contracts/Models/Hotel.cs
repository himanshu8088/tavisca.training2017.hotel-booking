using System;
using System.Collections.Generic;

namespace HotelEngine.Contracts.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public float StarRating { get; set; }
        public Fare Fare { get; set; }
        public Location Location { get; set; }
        public List<Uri> Images { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Amenity> Amenities { get; set; } 
    }
}