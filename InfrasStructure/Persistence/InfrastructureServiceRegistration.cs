

using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Persistence.IdentityDbcontext;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastrucreService(this IServiceCollection Services ,IConfiguration configuration)
        {
            Services.AddScoped<IDataSeed, DataSeed>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
           Services.AddDbContext<ApplicationDbContext>(Option =>
            {
                Option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddDbContext<ECommerceIdentityContext>(Option =>
            {
                Option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ECommerceIdentityContext>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
              return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });

            return Services;
        }

    }
}
