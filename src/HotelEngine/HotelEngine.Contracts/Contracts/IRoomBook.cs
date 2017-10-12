using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IRoomBook 
    {
        Task<RoomBookRS> RoomBookAsync(RoomBookRQ roomBookRequest);
    }
}
