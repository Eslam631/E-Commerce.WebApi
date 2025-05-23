using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) 
        
        {
           services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(Option =>
            {
                Option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Enter Bearer Follow By Your Token"

                });

                Option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                         Reference=new OpenApiReference
                         {
                              Id="Bearer",
                               Type= ReferenceType.SecurityScheme
                         },


                    },
                    new string []{}
                    }
                });


            });
            return services;
        
        }

        public static IServiceCollection AddWepApplicationService(this IServiceCollection Services) {

          Services.Configure<ApiBehaviorOptions>((Option) =>
            {
                Option.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;


            });

            return Services;

        }

        public static IServiceCollection AddJwtService(this IServiceCollection Services,IConfiguration configuration) {


            Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



            }).AddJwtBearer(Option =>
            Option.TokenValidationParameters = new TokenValidationParameters() {
                 ValidateIssuer=true,
                ValidIssuer = configuration["JWTOption:Issuer"],
              ValidateAudience=true,
                ValidAudience = configuration["JWTOption:Audience"],
                ValidateLifetime=true,
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTOption")["SecretKey"]))

            }


            );

            return Services;
        
        }

    }
}
