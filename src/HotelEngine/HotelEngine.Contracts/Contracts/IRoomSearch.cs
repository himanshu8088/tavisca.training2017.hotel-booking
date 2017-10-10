﻿using HotelEngine.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelEngine.Contracts.Contracts
{
    public interface IRoomSearch
    {
        Task<RoomSearchRS> SearchAsync(RoomSearchRQ roomSearchRQ);
    }
}
