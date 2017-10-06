﻿using BusinessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.ContractsImplementation;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Factories
{
    public class Factory
    {
        private static Dictionary<Type, object> _mapping = new Dictionary<Type, object>()
        {
            { typeof(IHotelSearch),   new HotelSearch()  },            
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

