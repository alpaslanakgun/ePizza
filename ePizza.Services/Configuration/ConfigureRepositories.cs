using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Data.Concrete.EntityFramework.Contexts;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Implementations;
using ePizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ePizza.Services.Configuration
{
    public class ConfigureRepositories
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ePizzaContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            services.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ePizzaContext>();

            services.AddScoped<DbContext, ePizzaContext>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<Category>, Repository<Category>>();
            services.AddTransient<IRepository<ProductType>, Repository<ProductType>>();
            services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
            services.AddTransient<IRepository<OrderItem>, Repository<OrderItem>>();
            services.AddTransient<IRepository<PaymentDetails>, Repository<PaymentDetails>>();
        }
    }
}
