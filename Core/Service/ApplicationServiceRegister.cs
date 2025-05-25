using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
       services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider=>
            ()=>provider.GetRequiredService<IProductService>());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<Func<IAuthService>>(provider =>
            () => provider.GetRequiredService<IAuthService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());


            services.AddScoped<ICacheService, CacheService>();



            return services;


        }

    }
}
