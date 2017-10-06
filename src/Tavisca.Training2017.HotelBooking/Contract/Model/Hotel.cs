using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class Hotel
    {        
        public string HotelName { get; set; }
        public string Address { get; set; }
        public float StarRating { get; set; }        
        public decimal BaseFare { get; set; }
        public List<Uri> MediaUri { get; set; }
    }
}
