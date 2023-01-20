using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Shared.Entities.Abstract;

namespace ePizza.Entities.Concrete
{
    public class ProductType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
