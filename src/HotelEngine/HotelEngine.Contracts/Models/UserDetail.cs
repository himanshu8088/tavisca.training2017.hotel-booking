using System;

namespace HotelEngine.Contracts.Models
{
    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
    }
}