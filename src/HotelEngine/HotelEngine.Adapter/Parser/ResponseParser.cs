using BookingProxy;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Parser
{
    public class ResponseParser
    {
        private string[] _hotelFareSource;
        public ResponseParser()
        {
            _hotelFareSource = new string[]{
                "HotelBeds Test", "TouricoTGSTest"
            };
        }

        internal HotelEngine.Contracts.Models.HotelSearchRS ParseHotelSearchRS(Proxies.HotelSearchRS hotelSearchRS, Guid sessionId)
        {
            HotelEngine.Contracts.Models.HotelSearchRS hotelSearchResponse = null;
            var itineraries = hotelSearchRS.Itineraries;

            var hotels = new List<Hotel>();
            var rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var itinerary in itineraries)
            {
                var hotelProp = itinerary.HotelProperty;

                if (itinerary.HotelFareSource.Name.Equals(_hotelFareSource[0]) || itinerary.HotelFareSource.Name.Equals(_hotelFareSource[1]))
                {
                    foreach (var roomProp in itinerary.Rooms)
                    {
                        var room = new HotelEngine.Contracts.Models.Room()
                        {
                            Description = roomProp.RoomDescription,
                            RoomId = roomProp.RoomId,
                            Fare = new HotelEngine.Contracts.Models.Fare()
                            {
                                BaseFare = roomProp.DisplayRoomRate.BaseFare.Amount,
                                Currency = roomProp.DisplayRoomRate.BaseFare.Currency
                            },
                            Name = roomProp.RoomName,
                            Type = roomProp.RoomType,
                            Bed = roomProp.BedType
                        };
                        rooms.Add(room);
                    }

                    List<Uri> urls = new List<Uri>();
                    foreach (var media in hotelProp.MediaContent)
                    {
                        var url = new Uri(media.Url);
                        urls.Add(url);
                    }

                    List<HotelEngine.Contracts.Models.Amenity> amenities = new List<HotelEngine.Contracts.Models.Amenity>();
                    foreach (var hotelAmenity in hotelProp.Amenities)
                    {
                        var amenity = new HotelEngine.Contracts.Models.Amenity()
                        {
                            Name = hotelAmenity.Name,
                            Id = hotelAmenity.Id,
                        };
                        amenities.Add(amenity);
                    }

                    List<HotelEngine.Contracts.Models.HotelDescription> hotelDescriptions = new List<HotelEngine.Contracts.Models.HotelDescription>();
                    foreach (var description in hotelProp.Descriptions)
                    {
                        var hotelDescription = new HotelEngine.Contracts.Models.HotelDescription()
                        {
                            Type = description.Type,
                            Description = description.Description
                        };
                        hotelDescriptions.Add(hotelDescription);
                    }

                    var hotel = new Hotel()
                    {
                        Address = hotelProp.Address.CompleteAddress,
                        Fare = new HotelEngine.Contracts.Models.Fare()
                        {
                            Currency = itinerary.Fare.BaseFare.Currency,
                            BaseFare = itinerary.Fare.BaseFare.Amount
                        },
                        HotelId = hotelProp.Id,
                        HotelName = hotelProp.Name,
                        GeoCode = new HotelEngine.Contracts.Models.GeoCode()
                        {
                            Latitude = hotelProp.Address.GeoCode.Latitude,
                            Longitude = hotelProp.Address.GeoCode.Latitude
                        },
                        Rooms = rooms,
                        StarRating = hotelProp.HotelRating.Rating,
                        Images = urls,
                        Amenities = amenities,
                        HotelDescriptions = hotelDescriptions
                    };
                    hotels.Add(hotel);
                }
            }

            hotelSearchResponse = new HotelEngine.Contracts.Models.HotelSearchRS()
            {
                SessionId = sessionId.ToString(),
                Hotels = hotels
            };
            return hotelSearchResponse;
        }



        internal RoomSearchRS ParseRoomSearchRS(Proxies.HotelRoomAvailRS roomSearchResponse, Guid sessionId)
        {
            RoomSearchRS roomSearchRS = null;
            List<HotelEngine.Contracts.Models.Room> rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var roomResult in roomSearchResponse.Itinerary.Rooms)
            {
                HotelEngine.Contracts.Models.Room room = null;
                if (roomResult.HotelFareSource.Name.Equals(_hotelFareSource[0]) || roomResult.HotelFareSource.Name.Equals(_hotelFareSource[1]))
                {
                    room = new HotelEngine.Contracts.Models.Room()
                    {
                        Bed = roomResult.BedType,
                        Description = roomResult.RoomDescription,
                        Fare = new HotelEngine.Contracts.Models.Fare()
                        {
                            BaseFare = roomResult.DisplayRoomRate.BaseFare.Amount,
                            Currency = roomResult.DisplayRoomRate.BaseFare.Currency
                        },
                        RoomId = roomResult.RoomId,
                        Name = roomResult.RoomName,
                        Type = roomResult.RoomType
                    };
                    rooms.Add(room);
                }
            }

            var images = new List<Uri>();
            foreach (var media in roomSearchResponse.Itinerary.HotelProperty.MediaContent)
            {
                var imgUri = new Uri(media.Url);
                images.Add(imgUri);
            }

            roomSearchRS = new RoomSearchRS()
            {
                Rooms = rooms,
                SessionId = sessionId.ToString(),
                HotelId = roomSearchResponse.Itinerary.HotelProperty.Id,
                Images = images
            };
            return roomSearchRS;
        }



        internal RoomPriceSearchRS ParseRoomPriceSearchRS(TripProductPriceRS tripProductPriceRS)
        {
            RoomPriceSearchRS roomPriceSearchRS = null;
            var hotel = (HotelTripProduct)tripProductPriceRS.TripProduct;
            var room = hotel.HotelItinerary.Rooms[0];
            var hotelId = hotel.HotelItinerary.HotelProperty.Id;

            roomPriceSearchRS = new RoomPriceSearchRS()
            {
                ChargebleFare = new HotelEngine.Contracts.Models.Fare()
                {
                    BaseFare = room.DisplayRoomRate.BaseFare.Amount,
                    Currency = room.DisplayRoomRate.BaseFare.Currency,
                    TotalFare = room.DisplayRoomRate.TotalFare.Amount
                },
                SessionId = tripProductPriceRS.SessionId,
                HotelId = hotelId
            };

            return roomPriceSearchRS;
        }



        internal RoomBookRS ParseRoomBookRS(CompleteBookingRS completeBookingRS)
        {
            var tripFolder = completeBookingRS.TripFolder;
            var fare = tripFolder.Products[0].PaymentBreakups[0].Amount;
            var status = completeBookingRS.ServiceStatus.Status.ToString();

            return new RoomBookRS
            {
                BookingId = tripFolder.ConfirmationNumber,
                ConfirmationNo = completeBookingRS.TripFolder.Products[0].PassengerSegments[0].VendorConfirmationNumber,
                FareCharged = new HotelEngine.Contracts.Models.Fare()
                {
                    TotalFare = fare.Amount,
                    Currency = fare.Currency
                },
                TransactionDateTime = DateTime.Now,

            };
        }
    }
}
