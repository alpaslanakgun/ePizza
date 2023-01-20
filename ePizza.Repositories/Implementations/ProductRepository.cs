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
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }
    }
}
