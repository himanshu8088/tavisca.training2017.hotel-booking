using BookingProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Configuration
{
    public class CompleteBookConfig
    {
        private Money _fare;
        private Payment _payment;
        private CreditCardPayment _creditCardPayment;
        private string _sessionId;

        public CompleteBookConfig(TripFolderBookRS tripFolderBookRS, Guid sessionId)
        {
            _fare = ((HotelTripProduct)tripFolderBookRS.TripFolder.Products[0]).HotelItinerary.Rooms[0].DisplayRoomRate.TotalFare;
            _payment = tripFolderBookRS.TripFolder.Payments[0];
            _creditCardPayment = (CreditCardPayment)tripFolderBookRS.TripFolder.Payments[0];
            _sessionId = sessionId.ToString();
        }

        public CreditCardPayment ExternalPayment => new CreditCardPayment()
        {
            Amount = _fare,
            Attributes = new StateBag[]
                    {
                            new StateBag() { Name="API_SESSION_ID", Value=_sessionId},
                            new StateBag(){ Name="PointOfSaleRule"},
                            new StateBag(){ Name="SectorRule"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Username"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Password"},
                            new StateBag(){ Name="AmountToAuthorize",Value=_fare.Amount.ToString()},
                            new StateBag(){ Name="PaymentStatus",Value="Authorization successful"},
                            new StateBag(){ Name="AuthorizationTransactionId",Value=Guid.NewGuid().ToString()},
                            new StateBag(){ Name="ProviderAuthorizationTransactionId",Value=Guid.NewGuid().ToString()},
                            new StateBag(){ Name="PointOfSaleRule"},
                            new StateBag(){ Name="SectorRule"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Username"},
                            new StateBag(){ Name="_AttributeRule_Rovia_Password"}
                    },
            BillingAddress = _payment.BillingAddress,
            CardMake = _creditCardPayment.CardMake,
            CardType = _creditCardPayment.CardType,
            ExpiryMonthYear = _creditCardPayment.ExpiryMonthYear,
            NameOnCard = _creditCardPayment.NameOnCard,
            IsThreeDAuthorizeRequired = false,
            Number = _creditCardPayment.Number
        };     
    }
}
