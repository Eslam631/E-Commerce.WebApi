
using Domain.Contracts;
using E_Commerce.Web.CustomModelWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Context;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErroeModels;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

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
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
