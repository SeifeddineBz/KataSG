﻿using DistributeurBoisson.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.BLL.IService
{
    public interface IRecetteService
    {
       public double CalculatePriceRecette(string recetteName);
    }
}
