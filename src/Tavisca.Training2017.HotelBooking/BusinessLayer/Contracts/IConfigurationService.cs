﻿using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Contracts
{
    public interface IConfigurationService
    {
        PointOfSale GetPointofSale(int pointOfsaleId);
    }    
}
