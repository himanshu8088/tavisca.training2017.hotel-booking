using Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelEngine.Adapter.Engines
{
    internal class HotelEngine
    {
        private HotelEngineClient _hotelEngineClient = null;

        internal async Task<Proxies.HotelSearchRS> GetHotelsAsync(Proxies.HotelSearchRQ request)
        {
            Proxies.HotelSearchRS hotelSearchRS;
            try
            {
                _hotelEngineClient = new Proxies.HotelEngineClient();
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

        internal async Task<Proxies.HotelRoomAvailRS> GetRoomsAsync(Proxies.HotelRoomAvailRQ hotelRoomAvailRQ)
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

    }
}
