using Domain.Contracts;
using E_Commerce.Web.CustomModelWares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

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
            app.UseSwaggerUI(Option=>
            {
                Option.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true
                };
                Option.DocumentTitle = "Ny E-Commerce Api";

                Option.JsonSerializerOptions = new JsonSerializerOptions()
                {
                     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                Option.DocExpansion(DocExpansion.None);
                Option.EnableFilter();
                Option.EnablePersistAuthorization();

            });
            return app;
        }
    }
}
