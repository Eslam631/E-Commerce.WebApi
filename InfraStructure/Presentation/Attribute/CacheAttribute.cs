using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attribute
{
    internal class CacheAttribute(int DurationInSec=90):ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //create CacheKey

            string CacheKey = CreateCacheKey(context.HttpContext.Request);

            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            //search Value Key with cache Key 
            var CacheValue=await cacheService.GetAsync(CacheKey);

            if(CacheValue != null)
            {
                context.Result = new ContentResult() {
                
                     Content = CacheValue,
                      ContentType="application/json",
                       StatusCode=StatusCodes.Status200OK
                
                
                };

                return;
            }
            //Return value if is Null
          var ExecutedContext=await  next.Invoke();

            //Set Value With Cache Key

            if (ExecutedContext.Result is OkObjectResult result) {

             await   cacheService.SetAsync(CacheKey, result.Value,TimeSpan.FromSeconds(DurationInSec));
            
            }

        }

        private static string CreateCacheKey(HttpRequest  request)
        {

            StringBuilder key= new StringBuilder();
            key.Append(request.Path + "?");

            foreach (var item in request.Query.OrderBy(Q=>Q.Key)) {
            
                key.Append($"{item.Key}={item.Value}&");
            
            
            }


            return key.ToString();
             
           
        }
    }
}
