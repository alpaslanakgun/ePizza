﻿using ePizza.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Services.Entities.Abstract;

namespace ePizza.Entities.Dtos
{
    public class ProductListDto:DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}