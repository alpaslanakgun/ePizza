using ePizza.Services.Implementations;
using ePizza.Services.Interfaces;
using ePizza.WebUI.Helpers;
using ePizza.WebUI.Helpers.Interfaces;
using ePizza.WebUI.Interfaces;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ePizza.WebUI.Configuration
{
    public class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationManager>();

            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IPaymentService, PaymentManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<IProductService,ProductManager>();
            services.AddTransient<IProductTypeService,ProductTypeManager>();
            services.AddTransient<ICartService, CartManager>();
            services.AddTransient<IOrderService, OrderManager>();
            services.AddTransient<IFileHelper, FileHelper>();
        }
    }
}
