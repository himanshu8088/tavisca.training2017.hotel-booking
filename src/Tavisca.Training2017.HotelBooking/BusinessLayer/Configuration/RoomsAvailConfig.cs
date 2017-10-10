using System;
using System.Collections.Generic;
using System.Text;
using Connector;

namespace BusinessLayer.Configuration
{
    public class RoomsAvailConfig
    {        
        public ResponseType ResultRequested => ResponseType.Unknown;

        public HotelSearchCriterion HotelSearchCriterion => new HotelSearchCriterion()
        {
            Attributes = new StateBag[]
            {
                new StateBag(){ Name="FareType", Value="basefare"}
            },

            MatrixResults = true,
            
            MaximumResults=1500,

            Pos=new PointOfSale()
            {
                AdditionalInfo=new StateBag[]
                {
                    new StateBag(){Name="IPAddress", Value="127.0.0.1"},
                    new StateBag(){Name="DealerUrl", Value="localhost"},
                    new StateBag(){Name="SiteUrl", Value="ota"},
                    new StateBag(){Name="AccountId",Value="169050"},
                    new StateBag(){Name="UserId",Value="3285301"},
                    new StateBag(){Name="CountryName",Value="United States"},
                    new StateBag(){Name="CountryCode",Value="US"},
                    new StateBag(){Name="UserProfileCountryCode",Value="US"},
                    new StateBag(){Name="CustomerType",Value="DTP"},
                    new StateBag(){Name="DKCommissionIdentifier",Value="3285301P"},
                    new StateBag(){Name="MemberSignUpDate",Value="Tue, 04 Jan 2011"}
                },
                PosId=101,
                Requester=new Company()
                {
                    Agency=new Agency()
                    {
                        AgencyAddress=new Address()
                        {
                            CodeContext=LocationCodeContext.Address,
                            City=new City()
                            {
                                CodeContext=LocationCodeContext.City,
                                Name="Nevada",
                                Country="US",
                                State="NV"
                            },
                            ZipCode="89002"
                        },
                        AgencyId=0,
                        AgencyName="WV"                        
                    },
                    Code="DTP",
                    CodeContext= CompanyCodeContext.PersonalTravelClient,
                    DK= "3285301P",
                    FullName= "Rovia"
                }
            },
            PriceCurrencyCode="INR",
            Guests=new PassengerTypeQuantity[]
            {
                new PassengerTypeQuantity()
                {
                    Ages=new int[]
                    {
                        30,30
                    },
                    PassengerType=PassengerType.Adult,
                    Quantity=2
                }
            },
            IsReturnRooms=false,
            Location=new Location()
            {
                CodeContext=LocationCodeContext.City,
                GeoCode=new GeoCode()
                {
                    Latitude= 36.11093f,
                    Longitude= -115.16935f
                } ,
                Name= "Las Vegas"                  
            },
            ProcessingInfo=new HotelSearchProcessingInfo()
            {
                DisplayOrder= HotelDisplayOrder.ByPriceHighToLow
            },
            RoomOccupancyTypes=new RoomOccupancyType[]
            {
                new RoomOccupancyType()
                {
                    PaxQuantities=new PassengerTypeQuantity[]
                    {
                        new PassengerTypeQuantity()
                        {
                            Ages =new int[]{
                                30,30
                            },
                            PassengerType=PassengerType.Adult,
                            Quantity=2
                        }                        
                    }                    
                }                
            },
            StayPeriod=new DateTimeSpan()
            {
                End=DateTime.Parse("2017-10-26T00:00:00"),
                Start= DateTime.Parse("2017-10-25T00:00:00")
            }            
        };

        public HotelItinerary HotelItinerary => new HotelItinerary()
        {
            HotelProperty = new HotelProperty()
            {
                Id= 616956
            }
        };
    }
}
