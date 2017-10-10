namespace HotelEngine.Contracts.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Fare Fare { get; set; }
        public string Bed { get; set; }
    }
}