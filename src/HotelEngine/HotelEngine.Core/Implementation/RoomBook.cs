using HotelEngine.Adapter;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelEngine.Core.Implementation
{
    public class RoomBook : IRoomBook
    {
        private IHotelConnector _hotelConnector;

        public RoomBook()
        {
            _hotelConnector = new HotelConnector();
        }
        public async Task<RoomBookRS> RoomBookAsync(RoomBookRQ roomBookRQ)
        {
            var roomBookRS = await _hotelConnector.RoomBookAsync(roomBookRQ);
            return roomBookRS;
        }
    }
}
