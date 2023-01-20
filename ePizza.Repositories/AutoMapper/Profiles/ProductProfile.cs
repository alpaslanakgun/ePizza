using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos;

namespace ePizza.Repositories.AutoMapper.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        { 
            
            CreateMap<ProductAddDto, Product>();

            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

        }
    }
}
