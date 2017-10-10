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
            { typeof(IHotelSearch), new HotelSearch() },
            { typeof(IRoomSearch), new RoomSearch() }
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
