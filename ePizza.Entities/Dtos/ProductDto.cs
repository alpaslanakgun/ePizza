using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using ePizza.Services.Entities.Abstract;

namespace ePizza.Entities.Dtos
{
    public class ProductDto:DtoGetBase
    {
        public Product Product  { get; set; }
    }
}
