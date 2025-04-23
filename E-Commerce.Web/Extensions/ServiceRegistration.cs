using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) 
        
        {
           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();
            return services;
        
        }

        public static IServiceCollection AddWepApplicationService(this IServiceCollection Services) {

          Services.Configure<ApiBehaviorOptions>((Option) =>
            {
                Option.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;


            });

            return Services;

        }


    }
}
