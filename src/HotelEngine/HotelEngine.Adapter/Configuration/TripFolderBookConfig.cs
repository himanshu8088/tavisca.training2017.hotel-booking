using System;
using System.Collections.Generic;
using System.Text;
using BookingProxy;

namespace HotelEngine.Adapter.Configuration
{
    public class TripFolderBookConfig
    {
        private string _tripFolderName;
        private int _age;
        private DateTime _birthdate;
        private Money _amount;
        private string _sessionId;
        private HotelItinerary _hotelItinerary;
        private HotelSearchCriterion _hotelSearchCriterion;
        private int[] _ages;
        private int _qty;
        private decimal _fareToAuthorise;

        public TripFolderBookConfig(TripFolderBookSettings tripFolderBookSettings)
        {
            _tripFolderName = tripFolderBookSettings.TripFolderName;
            _age = tripFolderBookSettings.Age;
            _birthdate = tripFolderBookSettings.Birthdate;
            _amount = tripFolderBookSettings.Amount;
            _sessionId = tripFolderBookSettings.SessionId;
            _ages = tripFolderBookSettings.Ages;
            _hotelItinerary = tripFolderBookSettings.HotelItinerary;
            _hotelSearchCriterion = tripFolderBookSettings.HotelSearchCriterion;
            _qty = tripFolderBookSettings.Qty;
            _fareToAuthorise = _hotelItinerary.Rooms[0].DisplayRoomRate.TotalFare.Amount;
            
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
                    Email = "sbejugam@v-worldventures.com",
                    FirstName = "Sandbox",
                    LastName = "Test",
                    MiddleName = "User",
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
                    Email = "sbejugam@v-worldventures.com",
                    FirstName = "Sandbox",
                    LastName = "Test",
                    MiddleName = "User",
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
                        BirthDate=_birthdate,
                        CustomFields=new StateBag[]
                        {
                            new StateBag(){ Name="Boyd Gaming"},
                            new StateBag(){ Name="IsPassportRequired" , Value="false"}
                        },
                        Email="rsarda@tavisca.com",
                        FirstName="Sandbox",
                        Gender=Gender.Male,
                        KnownTravelerNumber="789456",
                        LastName="Test",
                        PassengerType=PassengerType.Adult,
                        PhoneNumber="1111111111",
                        UserName="rsarda@tavisca.com"
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

                            //new StateBag(){ Name="AmountToAuthorize",Value=_fareToAuthorise.ToString()},
                            //new StateBag(){ Name="PaymentStatus",Value="Authorization successful"},
                            //new StateBag(){ Name="AuthorizationTransactionId",Value="975b4c93-228d-41dd-97ed-7d0b7b8c445b" },
                            //new StateBag(){ Name="ProviderAuthorizationTransactionId",Value="1F3FE4A1-0AB1-491E-BC18-20E71EFCDF7B" },
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
