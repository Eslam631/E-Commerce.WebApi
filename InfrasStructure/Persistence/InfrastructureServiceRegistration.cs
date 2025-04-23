

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastrucreService(this IServiceCollection Services ,IConfiguration configuration)
        {
            Services.AddScoped<IDataSeed, DataSeed>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
           Services.AddDbContext<ApplicationDbContext>(Option =>
            {
                Option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return Services;
        }

    }
}
