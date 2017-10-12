using HotelEngine.Contracts;
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
            { typeof(IHotelSearch), new Implementation.HotelSearch() },
            { typeof(IRoomSearch), new RoomSearch() },
            { typeof(IPriceSearch), new PriceSearch() },
            { typeof(IRoomBook), new RoomBook() }
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
