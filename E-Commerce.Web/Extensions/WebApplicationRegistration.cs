using Domain.Contracts;
using E_Commerce.Web.CustomModelWares;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app) {

            using var Scope = app.Services.CreateScope();
            var ObjectDate = Scope.ServiceProvider.GetRequiredService<IDataSeed>();

            await ObjectDate.IdentityDataSeedAsync();
            await ObjectDate.DataSeedAsync();
        }


        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandelWare>();
            return app;

        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app) {

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
