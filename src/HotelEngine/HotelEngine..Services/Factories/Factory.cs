using HotelEngine.Contracts.Contracts;
using System;
using System.Collections.Generic;

namespace HotelEngine.Services.Factories
{
    public class Factory
    {
        private static Dictionary<Type, object> _mapping = new Dictionary<Type, object>()
        {
            { typeof(IHotelService), new HotelService() },
          
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
