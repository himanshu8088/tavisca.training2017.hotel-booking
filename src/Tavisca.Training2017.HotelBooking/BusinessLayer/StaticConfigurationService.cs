using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Contracts;
using BusinessLayer.Model;

namespace BusinessLayer
{
    public class StaticConfigurationService : IConfigurationService
    {
        public PointOfSale GetPointofSale(int pointOfsaleId)
        {
            return new PointOfSale();
        }
    }
}
