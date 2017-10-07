using System;
using System.Collections.Generic;

namespace BusinessLayer.Model
{
    public class HotelItinerary
    {
       public Hotel Hotel { get; set; }
       public decimal BaseFare { get; set; }
       public List<Uri> MediaUri { get; set; }
       public Location Location { get; set; } 
    }
}
