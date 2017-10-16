using System;

namespace HotelEngine.Contracts.Models
{
    public class CardDetail
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }             
    }
}