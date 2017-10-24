using System;
using System.Collections.Generic;
using System.Text;
using BookingProxy;
using Newtonsoft.Json;
using HotelEngine.Contracts.Models;

namespace HotelEngine.Adapter.Configuration
{
    public class TripProductConfig
    {
        private HotelItinerary _hotelItinerary;
        private HotelSearchCriterion _hotelSearchCriterion;   

        public TripProductConfig(HotelSearchCriterion hotelSearchCriterion,HotelItinerary hotelItinerary, RoomPriceSearchRQ roomPriceSearchRQ)
        {
            _hotelItinerary = SelectRoomItinerary(hotelItinerary, roomPriceSearchRQ.RoomName);
            _hotelSearchCriterion = hotelSearchCriterion;            
        }

        private HotelItinerary SelectRoomItinerary(HotelItinerary itinerary, string roomName)
        {
            BookingProxy.Room selectedRoom = null;
            HotelItinerary selectedItinerary = null;
            foreach (var room in itinerary.Rooms)
            {
                if (room.RoomName.Equals(roomName))
                {
                    selectedRoom = room;
                    selectedRoom.DisplayRoomRate.TotalFare.DisplayCurrency = "USD";
                    break;
                }
            }
            selectedItinerary = itinerary;
            selectedItinerary.Rooms = null;
            selectedItinerary.Rooms = new BookingProxy.Room[]
            {
                selectedRoom
            };
            return selectedItinerary;
        }

        public BookingProxy.HotelSearchCriterion HotelSearchCriterion => _hotelSearchCriterion;
        public BookingProxy.HotelItinerary HotelItinerary => _hotelItinerary;
            
    }
}
