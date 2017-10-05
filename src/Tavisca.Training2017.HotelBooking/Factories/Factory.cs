using BusinessLayer;
using Connector;
using Connector.Contracts;
using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;


namespace Tavisca.Training2017.HotelBooking.Factories
{
    public class Factory
    {
        private static Dictionary<Type, object> _mapping = new Dictionary<Type, object>()
        {
            //{ typeof(IHotelService),   new HotelService()  },
            { typeof(IHotelConnector),   new HotelConnector()  },
            { typeof(IConfigurationService),   new StaticConfigurationService()  },
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

