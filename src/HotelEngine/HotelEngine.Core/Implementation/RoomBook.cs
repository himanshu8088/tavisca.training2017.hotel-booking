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
        private IHotelAdapter _hotelAdapter;

        public RoomBook(IHotelAdapter hotelAdapter)
        {
            _hotelAdapter = hotelAdapter;
        }
        public async Task<RoomBookRS> BookAsync(RoomBookRQ roomBookRQ)
        {
            var roomBookRS = await _hotelAdapter.BookRoomAsync(roomBookRQ);
            return roomBookRS;
         }
    }
}
