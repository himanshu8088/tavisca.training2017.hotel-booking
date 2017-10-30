using HotelEngine.Adapter;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Core.Implementation;
using System;
using System.Collections.Generic;

namespace HotelEngine.Core.Factories
{
    public class Factory
    {
        private static Dictionary<Type, object> _mapping = new Dictionary<Type, object>()
        {
            { typeof(IHotelSearch), new HotelSearch(new HotelAdapter()) },
            { typeof(IRoomSearch), new RoomSearch(new HotelAdapter()) },
            { typeof(IPriceSearch), new PriceSearch(new HotelAdapter()) },
            { typeof(IRoomBook), new RoomBook(new HotelAdapter()) }
        };

        public static object Get<T>()
        {
            object value = "";

            if (_mapping.ContainsKey(typeof(T)))
                return _mapping[typeof(T)];

            return value;
        }
    }
}
