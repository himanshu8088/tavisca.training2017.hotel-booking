using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class Amenity
    {
        public string AmenityType { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public Uri ImageUrl { get; set; }
    }
}
