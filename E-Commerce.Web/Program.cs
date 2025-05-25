using E_Commerce.Web.Extensions;
using Persistence;
using Service;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();

                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
          builder.Services.AddInfrastrucreService(builder.Configuration);
            builder.Services.AddApplicationServices();  

            builder.Services.AddWepApplicationService();
            builder.Services.AddJwtService(builder.Configuration);
          
            var app = builder.Build();

            #region DataSeeding

        await app.SeedDataBaseAsync();
            #endregion



            app.UseCustomExceptionMiddleWare();   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
