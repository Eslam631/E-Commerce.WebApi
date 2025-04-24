

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
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
              return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });
           Services.AddDbContext<ApplicationDbContext>(Option =>
            {
                Option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return Services;
        }

    }
}
