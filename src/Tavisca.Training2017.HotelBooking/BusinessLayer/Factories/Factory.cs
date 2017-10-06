using Connector;
using Connector.Contracts;
using System;
using System.Collections.Generic;
using BusinessLayer.Contracts;
using BusinessLayer.ContractsImplementation;

namespace BusinessLayer.Factories
{
    public class Factory
    {
        private static Dictionary<Type, object> _mapping = new Dictionary<Type, object>()
        {
            { typeof(IHotelSearch),   new HotelSearch()  },
            { typeof(IHotelConnector),   new HotelConnector()  },
            { typeof(IConnectorConfiguration),   new StaticConnectorConfiguration()  }
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

