using BookingProxy;
using HotelEngine.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace HotelEngine.Adapter.Engines
{
    internal class BookingEngine
    {
        private TripsEngineClient _tripEngineClient = null;

        internal async Task<TripProductPriceRS> GetRoomPriceAsync(TripProductPriceRQ tripProductPriceRQ)
        {
            TripProductPriceRS tripProductPriceRS = null;

            try
            {
                _tripEngineClient = new TripsEngineClient();
                tripProductPriceRS = await _tripEngineClient.PriceTripProductAsync(tripProductPriceRQ);
            }
            catch (Exception e)
            {
                throw new Exception($"Error Occured : {e.Message}");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return tripProductPriceRS;
        }

        internal async Task<TripFolderBookRS> CreateTripFolderBookAsync(TripFolderBookRQ tripFolderBookRQ)
        {
            
            TripFolderBookRS tripFolderBookRS = null;
            try
            {
                _tripEngineClient = new TripsEngineClient();
                tripFolderBookRS = await _tripEngineClient.BookTripFolderAsync(tripFolderBookRQ);
            }
            catch (Exception e)
            {
                throw new Exception($"Error Occured : {e.Message}");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return tripFolderBookRS;
        }



        internal async Task<CompleteBookingRS> CompleteBookingAsync(CompleteBookingRQ completeBookingRQ)
        {
            CompleteBookingRS completeBookingRS;
            try
            {
                _tripEngineClient = new TripsEngineClient();
                completeBookingRS = await _tripEngineClient.CompleteBookingAsync(completeBookingRQ);
            }
            catch (Exception e)
            {
                throw new Exception($"Error Occured : {e.Message}");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return completeBookingRS;
        }

    }
}
