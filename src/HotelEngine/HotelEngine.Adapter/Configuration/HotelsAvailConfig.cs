using Proxies;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Configuration
{
    public class HotelsAvailConfig
    {

        private DateTime _checkIn;
        private DateTime _checkOut;
        private int _posId;
        private int _passengerCount;
        private int _noOfRooms;
        private float _latitude;
        private float _longitude;
        /*string poi, DateTime checkIn, DateTime checkOut, int passengerCount = 1, int noOfRooms = 1, float latitude = 27.173891f, float longitude = 78.042068f, int posId = 101*/

        public HotelsAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            _posId = 101;
            _checkIn = hotelSearchRQ.CheckInDate;
            _checkOut = hotelSearchRQ.CheckOutDate;
            _passengerCount = hotelSearchRQ.GuestCount;
            _latitude = hotelSearchRQ.Location.Latitude;
            _longitude = hotelSearchRQ.Location.Longitude;
            _noOfRooms = hotelSearchRQ.NoOfRooms;
        }
        public HotelFilter[] Filters => new HotelFilter[]
        {
            new AvailabilityFilter()
            {
                ReturnOnlyAvailableItineraries = true
            }
        };
        public HotelSearchCriterion HotelSearchCriterion => new HotelSearchCriterion()
        {

            Pos = new PointOfSale()
            {
                PosId = _posId,
                Requester = new Company()
                {
                    Code = "DTP",
                    CodeContext = CompanyCodeContext.PersonalTravelClient,
                    DK = "3285301P",
                    FullName = "Rovia",
                    ID = 0
                },
                AdditionalInfo = new StateBag[]
                {
                    new StateBag() { Name = "IPAddress", Value = "127.0.0.1" },
                    new StateBag() { Name = "DealerUrl", Value = "localhost" },
                    new StateBag() { Name = "SiteUrl", Value = "ota" },
                    new StateBag() { Name = "AccountId", Value = "169050" },
                    new StateBag() { Name = "UserId", Value = "3285301" },
                    new StateBag() { Name = "CountryName", Value = "United States" },
                    new StateBag() { Name = "CountryCode", Value = "US" },
                    new StateBag() { Name = "UserProfileCountryCode", Value = "US" },
                    new StateBag() { Name = "CustomerType", Value = "DTP" },
                    new StateBag() { Name = "DKCommissionIdentifier", Value = "3285301P" },
                    new StateBag() { Name = "MemberSignUpDate", Value = "Tue, 04 Jan 2011" }
                }
            },
            MatrixResults = true,
            MaximumResults = 1500,
            PriceCurrencyCode = "INR",
            Location = new Location()
            {
                CodeContext = LocationCodeContext.GeoCode,
                GeoCode = new GeoCode() { Latitude = _latitude, Longitude = _longitude },
                //GmtOffsetMinutes = 0,
                //Id = 0,
                //Radius = new Distance()
                //{
                //    Amount = 30,
                //    From = LocationCodeContext.City,
                //    Unit = DistanceUnit.mi
                //}
            },
            NoOfRooms = _noOfRooms,
            Guests = new PassengerTypeQuantity[]
            {
                new PassengerTypeQuantity()
                {
                    //Ages = new int[2]
                    //{
                    //    30,30
                    //},
                    PassengerType = PassengerType.Adult,
                    Quantity = _passengerCount
                }
            },
            ProcessingInfo = new HotelSearchProcessingInfo()
            {
                DisplayOrder = HotelDisplayOrder.ByRelevanceScoreDescending
            },
            RoomOccupancyTypes = new RoomOccupancyType[]
            {
                new RoomOccupancyType()
                {
                    PaxQuantities =  new PassengerTypeQuantity[]
                                     {
                                            new PassengerTypeQuantity()
                                            {
                                                //Ages = new int[2]
                                                //{
                                                //    30,30
                                                //},
                                                PassengerType = PassengerType.Adult,
                                                Quantity = _passengerCount
                                            }
                                     }
                }
            },
            SearchType = HotelSearchType.City,
            StayPeriod = new DateTimeSpan()
            {
                Duration = 0,
                Start = _checkIn != null ? _checkIn : DateTime.Now.AddDays(5)/*DateTime.Parse("2017-10-26")*/,
                End = _checkOut != null ? _checkOut : DateTime.Now.AddDays(7)/*DateTime.Parse("2017-10-25")*/
            },
            Attributes = new StateBag[]
            {
                new StateBag() { Name = "FareType", Value = "BaseFare" },
                new StateBag() { Name = "ResetFiltersIfNoResults", Value = "true" },
                new StateBag() { Name = "ReturnRestrictedRelevanceProperties", Value = "true" },
                new StateBag() { Name = "MaxHideawayRelevancePropertiesToDisplay", Value = "5" },
                new StateBag() { Name = "MaxHotelRelevancePropertiesToDisplay", Value = "10" }
            }
        };
        public Address Address => new Address()
        {
            CodeContext = LocationCodeContext.Address,
            GmtOffsetMinutes = 0,
            City = new City()
            {
                CodeContext = LocationCodeContext.City,
                GmtOffsetMinutes = 0,
                Id = 0
            }
        };
        public Agency Agency => new Agency()
        {
            AgencyId = 0,
            AgencyName = "WV"
        };
        public PagingInfo PagingInfo => new PagingInfo()
        {
            Enabled = true,
            EndNumber = 120,
            StartNumber = 100,
            TotalRecordsBeforeFiltering = 0,
            TotalResults = 0
        };
        public ResponseType ResultRequested => ResponseType.Complete;
    }
}
