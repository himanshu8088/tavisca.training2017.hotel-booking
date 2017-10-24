using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Adapter.Contracts;
using BookingProxy;
using Newtonsoft.Json;

namespace HotelEngine.Adapter
{
    public class HotelAdapter : IHotelAdapter
    {
        private IAdapterConfiguration _config;
        private Proxies.HotelEngineClient _hotelEngineClient = null;
        private TripsEngineClient _tripEngineClient = null;

        public HotelAdapter()
        {
            _config = new StaticAdapterConfiguration();
        }

        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelAvailRQ = ParseHotelRQ(hotelSearchRQ);
            var hotelAvailRS = await GetHotelsAsync(hotelAvailRQ);
            var hotelSearchRS = ParseHotelRS(hotelAvailRS, hotelSearchRQ.SessionId);
            return hotelSearchRS;
        }

        public async Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomAvailRQ = ParseRoomRQ(roomSearchRQ);
            var roomAvailRS = await GetRoomsAsync(roomAvailRQ);
            var roomSearchRS = ParseRoomRS(roomAvailRS, roomSearchRQ.SessionId);
            return roomSearchRS;
        }

        public async Task<RoomPriceSearchRS> SearchPriceAsync(RoomPriceSearchRQ roomPriceRQ)
        {
            RoomPriceSearchRS roomPriceSearchRS = null;
            try
            {
                var tripProductPriceRQ = await ParsePriceRQ(roomPriceRQ);
                var tripProductPriceRS = await GetRoomPriceAsync(tripProductPriceRQ);
                roomPriceSearchRS = ParsePriceRS(tripProductPriceRS);
            }
            catch (Exception e)
            {
                throw new Exception();
            }

            return roomPriceSearchRS;
        }

        public async Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ)
        {
            var tripFolderBookRS = await GetTripFolderBookAsync(roomBookRQ);
            var rq = ParseCompleteBookingRQ(tripFolderBookRS, roomBookRQ.SessionId);
            var rs = await CompleteBookingAsync(rq);
            RoomBookRS roomBookRS = ParseRoomBookRS(rs, tripFolderBookRS);
            return roomBookRS;
        }

        
        private async Task<Proxies.HotelSearchRS> GetHotelsAsync(Proxies.HotelSearchRQ request)
        {
            global::Proxies.HotelSearchRS hotelSearchRS;
            try
            {
                _hotelEngineClient = new global::Proxies.HotelEngineClient();
                hotelSearchRS = await _hotelEngineClient.HotelAvailAsync(request);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return hotelSearchRS;
        }

        private async Task<Proxies.HotelRoomAvailRS> GetRoomsAsync(Proxies.HotelRoomAvailRQ hotelRoomAvailRQ)
        {
            Proxies.HotelRoomAvailRS hotelRoomAvailRS;
            try
            {
                _hotelEngineClient = new Proxies.HotelEngineClient();
                hotelRoomAvailRS = await _hotelEngineClient.HotelRoomAvailAsync(hotelRoomAvailRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return hotelRoomAvailRS;
        }

        private async Task<BookingProxy.TripProductPriceRS> GetRoomPriceAsync(TripProductPriceRQ tripProductPriceRQ)
        {
            TripProductPriceRS tripProductPriceRS = null;

            try
            {
                _tripEngineClient = new BookingProxy.TripsEngineClient();
                tripProductPriceRS = await _tripEngineClient.PriceTripProductAsync(tripProductPriceRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return tripProductPriceRS;
        }

        private async Task<TripFolderBookRS> GetTripFolderBookAsync(RoomBookRQ roomBookRQ)
        {
            TripFolderBookRS tripFolderBookRS = null;
            var tripProductPriceRQ = await ParsePriceRQ(roomBookRQ);
            var tripProductPriceRS = await GetRoomPriceAsync(tripProductPriceRQ);
            var tripProduct = (HotelTripProduct)tripProductPriceRS.TripProduct;
            var tripConfig = new TripFolderBookConfig(tripProduct, roomBookRQ);
            try
            {
                _tripEngineClient = new TripsEngineClient();
                tripFolderBookRS = await _tripEngineClient.BookTripFolderAsync(tripConfig.TripFolderBookRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to Create Book Folder");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return tripFolderBookRS;
        }

        private async Task<CompleteBookingRS> CompleteBookingAsync(CompleteBookingRQ completeBookingRQ)
        {
            CompleteBookingRS completeBookingRS;
            try
            {
                _tripEngineClient = new TripsEngineClient();
                completeBookingRS = await _tripEngineClient.CompleteBookingAsync(completeBookingRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return completeBookingRS;
        }


        private Proxies.HotelSearchRQ ParseHotelRQ(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelSettings = _config.GetHotelsAvailConfig(hotelSearchRQ);
            var hotelSearchReq = new global::Proxies.HotelSearchRQ()
            {
                HotelSearchCriterion = hotelSettings.HotelSearchCriterion,
                SessionId = hotelSearchRQ.SessionId.ToString(),
                Filters = hotelSettings.Filters,
                PagingInfo = hotelSettings.PagingInfo,
                ResultRequested = hotelSettings.ResultRequested
            };
            return hotelSearchReq;
        }

        private HotelEngine.Contracts.Models.HotelSearchRS ParseHotelRS(Proxies.HotelSearchRS hotelSearchRS, Guid sessionId)
        {
            HotelEngine.Contracts.Models.HotelSearchRS hotelSearchResponse = null;
            var itineraries = hotelSearchRS.Itineraries;

            var hotels = new List<Hotel>();
            var rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var itinerary in itineraries)
            {
                var hotelProp = itinerary.HotelProperty;
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
                    Location = new HotelEngine.Contracts.Models.Location()
                    {
                        Latitude = hotelProp.Address.GeoCode.Latitude,
                        Longitude = hotelProp.Address.GeoCode.Latitude
                    },
                    Rooms = rooms,
                    StarRating = hotelProp.HotelRating.Rating,
                    Images = urls,
                    Amenities = amenities
                };
                hotels.Add(hotel);
            }

            hotelSearchResponse = new HotelEngine.Contracts.Models.HotelSearchRS()
            {
                SessionId = sessionId.ToString(),
                Hotels = hotels
            };
            return hotelSearchResponse;
        }

        private Proxies.HotelRoomAvailRQ ParseRoomRQ(RoomSearchRQ roomSearchRQ)
        {
            var roomsSettings = _config.GetRoomsAvailConfig(roomSearchRQ);
            var hotelRoomAvailRQ = new Proxies.HotelRoomAvailRQ()
            {
                HotelSearchCriterion = roomsSettings.SearchCriterion,
                Itinerary = roomsSettings.HotelItinerary,
                ResultRequested = roomsSettings.ResultRequested,
                SessionId = roomSearchRQ.SessionId.ToString()
            };
            return hotelRoomAvailRQ;
        }

        private RoomSearchRS ParseRoomRS(Proxies.HotelRoomAvailRS roomSearchResponse, Guid sessionId)
        {
            RoomSearchRS roomSearchRS = null;
            List<HotelEngine.Contracts.Models.Room> rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var roomResult in roomSearchResponse.Itinerary.Rooms)
            {
                var room = new HotelEngine.Contracts.Models.Room()
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

            var images = new List<Uri>();
            foreach (var media in roomSearchResponse.Itinerary.HotelProperty.MediaContent)
            {
                var imgUri = new Uri(media.Url);
                images.Add(imgUri);
            }

            roomSearchRS = new RoomSearchRS()
            {
                Rooms = rooms,
                SessionId = sessionId.ToString(),/*roomSearchRQ.SessionId.ToString()*/
                HotelId = roomSearchResponse.Itinerary.HotelProperty.Id,
                Images = images
            };
            return roomSearchRS;
        }

        private async Task<TripProductPriceRQ> ParsePriceRQ(RoomPriceSearchRQ roomPriceSearchRQ)
        {
            var roomAvailRQ = ParseRoomRQ(roomPriceSearchRQ);
            var roomAvailRS = await GetRoomsAsync(roomAvailRQ);
            var settings = _config.GetTripProductConfig(roomPriceSearchRQ, roomAvailRS);

            TripProductPriceRQ tripProductPriceRQ = new TripProductPriceRQ()
            {
                ResultRequested = ResponseType.Unknown,
                SessionId = roomPriceSearchRQ.SessionId.ToString(),
                TripProduct = new HotelTripProduct()
                {
                    HotelItinerary = settings.HotelItinerary,
                    HotelSearchCriterion = settings.HotelSearchCriterion
                }
            };

            return tripProductPriceRQ;
        }

        private RoomPriceSearchRS ParsePriceRS(TripProductPriceRS tripProductPriceRS)
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
     
        private CompleteBookingRQ ParseCompleteBookingRQ(TripFolderBookRS tripFolderBookRS,Guid sessionId)
        {
            var settings = _config.GetCompleteBookConfig(tripFolderBookRS, sessionId);
            var rq = new CompleteBookingRQ()
            {
                TripFolderId = tripFolderBookRS.TripFolder.Id,
                SessionId = sessionId.ToString(),
                ExternalPayment=settings.ExternalPayment,
                ResultRequested = tripFolderBookRS.ResponseRecieved
            };
            return rq;
        }

        private RoomBookRS ParseRoomBookRS(CompleteBookingRS completeBookingRS, TripFolderBookRS tripFolderBookRS)
        {
            var tripFolder = tripFolderBookRS.TripFolder;
            var fare = tripFolder.Products[0].PaymentBreakups[0].Amount;
            var status = completeBookingRS.ServiceStatus.Status.ToString();

            return new RoomBookRS
            {
                ConfirmationNo = tripFolder.ConfirmationNumber,
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
