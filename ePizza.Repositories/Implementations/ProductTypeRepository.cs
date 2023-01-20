using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Repositories.Implementations
{
    public class ProductTypeRepository:Repository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
