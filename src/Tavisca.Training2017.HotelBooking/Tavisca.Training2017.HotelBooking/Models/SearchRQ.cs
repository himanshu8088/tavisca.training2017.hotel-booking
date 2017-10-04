using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tavisca.Training2017.HotelBooking.Models
{
    public class SearchRQ
    {
        public string SearchText { get; set; }           
        public DateTime CheckIn { get; set; }        
        public DateTime CheckOut { get; set; }        
    }
}
