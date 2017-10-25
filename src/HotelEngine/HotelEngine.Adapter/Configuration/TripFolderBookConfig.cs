using System;
using System.Collections.Generic;
using System.Text;
using BookingProxy;
using HotelEngine.Contracts.Models;

namespace HotelEngine.Adapter.Configuration
{
    public class TripFolderBookConfig
    {
        private string _tripFolderName;
        private int _age;
        private UserDetail _userDetail;
        private Money _amount;
        private string _sessionId;
        private HotelItinerary _hotelItinerary;
        private HotelSearchCriterion _hotelSearchCriterion;
        private int[] _ages;
        private int _qty;
        private decimal _fareToAuthorise;

        public TripFolderBookConfig(HotelTripProduct tripProduct, RoomBookRQ roomBookRQ)
        {
            _hotelItinerary = tripProduct.HotelItinerary;
            BookingProxy.Room room=_hotelItinerary.Rooms[0];

            room.DisplayRoomRate.BaseFare.DisplayCurrency = room.DisplayRoomRate.BaseFare.Currency;
            room.DisplayRoomRate.BaseFare.DisplayAmount = room.DisplayRoomRate.BaseFare.Amount;
            room.DisplayRoomRate.TotalFare.DisplayAmount = room.DisplayRoomRate.TotalFare.Amount;
            room.DisplayRoomRate.TotalFare.DisplayCurrency = room.DisplayRoomRate.TotalFare.Currency;
            room.DisplayRoomRate.DailyRates[0].DisplayCurrency = room.DisplayRoomRate.DailyRates[0].Currency;
            room.DisplayRoomRate.DailyRates[0].DisplayAmount = room.DisplayRoomRate.DailyRates[0].Amount;
            room.DisplayRoomRate.Taxes[0].DisplayAmount = room.DisplayRoomRate.Taxes[0].Amount;
            room.DisplayRoomRate.Taxes[0].DisplayCurrency = room.DisplayRoomRate.Taxes[0].Currency;
            room.DisplayRoomRate.TotalCommission.DisplayCurrency = room.DisplayRoomRate.TotalCommission.Currency;
            room.DisplayRoomRate.TotalCommission.DisplayAmount = room.DisplayRoomRate.TotalCommission.Amount;
            room.DisplayRoomRate.TotalDiscount.DisplayCurrency = room.DisplayRoomRate.TotalDiscount.Currency;
            room.DisplayRoomRate.TotalDiscount.DisplayAmount = room.DisplayRoomRate.TotalDiscount.Amount;
            room.DisplayRoomRate.TotalFare.DisplayCurrency = room.DisplayRoomRate.TotalFare.Currency;
            room.DisplayRoomRate.TotalFare.DisplayAmount = room.DisplayRoomRate.TotalFare.Amount;
            room.DisplayRoomRate.TotalTax.DisplayCurrency = room.DisplayRoomRate.TotalTax.Currency;
            room.DisplayRoomRate.TotalTax.DisplayAmount = room.DisplayRoomRate.TotalTax.Amount;
            
            _hotelItinerary.Fare.AvgDailyRate.DisplayAmount = _hotelItinerary.Fare.AvgDailyRate.Amount;
            _hotelItinerary.Fare.AvgDailyRate.DisplayCurrency = _hotelItinerary.Fare.AvgDailyRate.Currency;
            _hotelItinerary.Fare.BaseFare.DisplayAmount = _hotelItinerary.Fare.BaseFare.Amount;
            _hotelItinerary.Fare.BaseFare.DisplayCurrency = _hotelItinerary.Fare.BaseFare.Currency;
            _hotelItinerary.Fare.MaxDailyRate.DisplayAmount = _hotelItinerary.Fare.MaxDailyRate.Amount;
            _hotelItinerary.Fare.MaxDailyRate.DisplayCurrency = _hotelItinerary.Fare.MaxDailyRate.Currency;
            _hotelItinerary.Fare.MinDailyRate.DisplayAmount = _hotelItinerary.Fare.MinDailyRate.Amount;
            _hotelItinerary.Fare.MinDailyRate.DisplayCurrency = _hotelItinerary.Fare.MinDailyRate.Currency;
            _hotelItinerary.Fare.TotalCommission.DisplayAmount = _hotelItinerary.Fare.TotalCommission.Amount;
            _hotelItinerary.Fare.TotalCommission.DisplayCurrency = _hotelItinerary.Fare.TotalCommission.Currency;
            _hotelItinerary.Fare.TotalDiscount.DisplayAmount = _hotelItinerary.Fare.TotalDiscount.Amount;
            _hotelItinerary.Fare.TotalDiscount.DisplayCurrency = _hotelItinerary.Fare.TotalDiscount.Currency;
            _hotelItinerary.Fare.TotalFare.DisplayAmount = _hotelItinerary.Fare.TotalFare.Amount;
            _hotelItinerary.Fare.TotalFare.DisplayCurrency = _hotelItinerary.Fare.TotalFare.Currency;
            _hotelItinerary.Fare.TotalFee.DisplayAmount = _hotelItinerary.Fare.TotalFee.Amount;
            _hotelItinerary.Fare.TotalFee.DisplayCurrency = _hotelItinerary.Fare.TotalFee.Currency;
            _hotelItinerary.Fare.TotalTax.DisplayAmount = _hotelItinerary.Fare.TotalTax.Amount;
            _hotelItinerary.Fare.TotalTax.DisplayCurrency = _hotelItinerary.Fare.TotalTax.Currency;

            _age = 30;/*DateTime.Now.Year - roomBookRQ.GuestDetail.DOB.Year;*/
            _userDetail = roomBookRQ.GuestDetail;
            _hotelSearchCriterion = tripProduct.HotelSearchCriterion;
            _sessionId = roomBookRQ.SessionId.ToString();
            _tripFolderName = $"TripFolder{DateTime.Now.Date}";
            _qty = roomBookRQ.GuestCount;
            _amount = tripProduct.HotelItinerary.Rooms[0].DisplayRoomRate.TotalFare;
            _amount.DisplayCurrency = "USD";
            _ages = new int[] { _age };
            _fareToAuthorise = this._hotelItinerary.Rooms[0].DisplayRoomRate.TotalFare.Amount;
        }

        public TripFolderBookRQ TripFolderBookRQ => new TripFolderBookRQ()
        {
            TripFolder = new TripFolder()
            {
                Creator = new User()
                {
                    AdditionalInfo = new StateBag[]
                    {
                        new StateBag(){ Name="AgencyName", Value="WV"},
                        new StateBag(){ Name="CompanyName", Value= "Rovia"},
                        new StateBag(){ Name="UserType", Value="Normal"}
                    },
                    Email = _userDetail.EmailId,
                    FirstName = _userDetail.FirstName,
                    LastName = _userDetail.LastName,
                    MiddleName = "",
                    Prefix = "Mr.",
                    Title = "Mr",
                    UserId = 169050,
                    UserName = "3285301"
                },
                FolderName = _tripFolderName,
                Owner = new User()
                {
                    AdditionalInfo = new StateBag[]
                    {
                        new StateBag(){ Name="AgencyName", Value="WV"},
                        new StateBag(){ Name="CompanyName", Value= "Rovia"},
                        new StateBag(){ Name="UserType", Value="Normal"}
                    },
                    Email = _userDetail.EmailId,
                    FirstName = _userDetail.FirstName,
                    LastName = _userDetail.LastName,
                    MiddleName = "",
                    Prefix = "Mr.",
                    Title = "Mr",
                    UserId = 169050,
                    UserName = "3285301"
                },
                Pos = new PointOfSale()
                {
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
                    },
                    PosId = 101,
                    Requester = new Company()
                    {
                        Agency = new Agency()
                        {
                            AgencyAddress = new Address()
                            {
                                CodeContext = LocationCodeContext.Address,
                                AddressLine1 = "Test1",
                                AddressLine2 = "Test2",
                                ZipCode = "89002"
                            },
                            AgencyName = "WV",
                        },
                        Code = "DTP",
                        CodeContext = CompanyCodeContext.Airline,
                        DK = "3285301P",
                        FullName = "Rovia"
                    },
                },
                Type = TripFolderType.Personal,
                Passengers = new Passenger[]
                {
                    new Passenger()
                    {
                        Age=_age,
                        BirthDate=_userDetail.DOB,
                        CustomFields=new StateBag[]
                        {
                            new StateBag(){ Name="Boyd Gaming"},
                            new StateBag(){ Name="IsPassportRequired" , Value="false"}
                        },
                        Email=_userDetail.EmailId,
                        FirstName=_userDetail.FirstName,
                        Gender=Gender.Male,
                        KnownTravelerNumber=_userDetail.MobileNo,
                        LastName=_userDetail.LastName,
                        PassengerType=PassengerType.Adult,
                        PhoneNumber=_userDetail.MobileNo,
                        UserName=_userDetail.EmailId
                    }
                },
                Payments = new CreditCardPayment[]
                {
                    new CreditCardPayment()
                    {
                        PaymentType=PaymentType.Credit,
                        Amount=_amount,
                        Attributes=new StateBag[]
                        {
                            new StateBag() { Name="API_SESSION_ID", Value=_sessionId},
                            new StateBag(){ Name="PointOfSaleRule"},
                            new StateBag(){ Name="SectorRule"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Username"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Password"},
                        },
                        BillingAddress=new Address()
                        {
                            CodeContext=LocationCodeContext.Address,
                            AddressLine1="E Sunset Rd",
                            AddressLine2="Near Trade Center",
                            City=new City()
                            {
                                CodeContext=LocationCodeContext.City,
                                Name="LAS",
                                Country="US",
                                State="State",
                            },
                            PhoneNumber="123456",
                            ZipCode="123456"
                        } ,
                        CardMake=new CreditCardMake()
                        {
                            Code="VI",
                            Name="VISA"
                        },
                        CardType=CreditCardType.Personal,
                        ExpiryMonthYear=DateTime.Parse("2020-12-01T00:00:00"),
                        IsThreeDAuthorizeRequired=false,
                        NameOnCard="test card",
                        Number="0000000000001111",
                        SecurityCode="123"
                    }
                },
                Products = new HotelTripProduct[]
                {
                   new HotelTripProduct()
                   {
                       Attributes=new StateBag[]
                       {
                           new StateBag{ Name ="API_SESSION_ID", Value=_sessionId},
                           new StateBag{ Name ="token", Value=""},
                           new StateBag{ Name ="ChargingHoursPriorToCPW", Value="48"},
                           new StateBag{ Name ="IsLoginWhileSearching", Value="Y"},
                           new StateBag{ Name ="IsInsuranceSelected", Value="no"},
                       },
                       /*Id=Guid.Parse("372c3e4b-7e20-4590-8e83-2a0c54f3303f"),*/
                       IsOnlyLeadPaxInfoRequired=true,
                       Owner=new User()
                       {
                           AdditionalInfo=new StateBag[]
                           {
                               new StateBag(){Name="AgencyName", Value="WV"},
                               new StateBag(){ Name="CompanyName", Value="Rovia"},
                               new StateBag(){ Name="UserType", Value="Normal"}
                           },
                            Email = "sbejugam@v-worldventures.com",
                            FirstName = "Sandbox",
                            LastName = "Test",
                            MiddleName = "User",
                            Prefix = "Mr.",
                            Title = "Mr",
                            UserId = 169050,
                            UserName = "3285301"
                       },
                       PassengerSegments=new PassengerSegment[]
                       {
                           new PassengerSegment()
                           {
                               BookingStatus=TripProductStatus.Planned,
                               PostBookingStatus=PostBookingTripStatus.None,
                               Rph=4
                           }
                       },
                       PaymentBreakups=new PaymentBreakup[]
                       {
                           new PaymentBreakup()
                           {
                               Amount=_amount                               
                           }
                       },
                       PaymentOptions=new PaymentType[]
                       {
                           PaymentType.SoftCash,
                           PaymentType.External,
                           PaymentType.Credit
                       },
                       Rph=4,
                       HotelItinerary=_hotelItinerary,
                       HotelSearchCriterion=_hotelSearchCriterion,
                       RoomOccupancyTypes=new RoomOccupancyType[]
                       {
                           new RoomOccupancyType()
                           {
                               PaxQuantities=new PassengerTypeQuantity[]
                               {
                                   new PassengerTypeQuantity()
                                   {
                                       Ages=_ages,
                                       PassengerType=PassengerType.Adult,
                                       Quantity=_qty
                                   }
                               }
                           }
                       }
                   }
                },
                Status = TripStatus.Planned,
            },
            TripProcessingInfo = new TripProcessingInfo()
            {
                TripProductRphs = new int[] { 4 }
            }
        };
    }
}
