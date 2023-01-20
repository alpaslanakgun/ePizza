using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Shared.Entities.Abstract;

namespace ePizza.Entities.Concrete
{
    public class Product:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int ProductTypeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
