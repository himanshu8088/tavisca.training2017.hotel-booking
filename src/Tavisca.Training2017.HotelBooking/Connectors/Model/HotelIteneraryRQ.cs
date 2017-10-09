using System;
using System.Collections.Generic;
using System.Text;

namespace Connector.Model
{
    public class HotelIteneraryRQ
    {        
        public HotelFilter[] Filters { get; set; }
        public HotelSearchCriterion HotelSearchCriterion { get; set; }
        public ResponseType ResultRequested { get; set; }                
        public PagingInfo PagingInfo { get; set; }  
        public string SessionId { get; set; }
        public string HotelId { get; set; }
    }
}
