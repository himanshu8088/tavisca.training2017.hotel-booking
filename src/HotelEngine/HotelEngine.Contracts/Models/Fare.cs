using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Contracts.Models
{
    public class Fare
    {
        public decimal TotalFare { get; set; }     
        public decimal BaseFare { get; set; }    
        public string Currency { get; set; }
    }
}
