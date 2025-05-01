using Domain.Exceptions;
using E_Commerce.Web.ErroeModels;

namespace E_Commerce.Web.CustomModelWares
{
    public class CustomExceptionHandelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandelWare> _logger;

        public CustomExceptionHandelWare(RequestDelegate next,ILogger<CustomExceptionHandelWare> logger )
        {
           _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandelNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                //
                _logger.LogError(ex, "SomeThing Went Wrong");

                //Change StateCode

                await HandelExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {


            var Response = new ErrorToReturn()
            {
                StatCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message,
            };
            httpContext.Response.StatusCode = ex switch
            {

                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException=> StatusCodes.Status401Unauthorized,
                BadRequestException badRequest=> GetBadRequestErrors(badRequest,Response) ,
                _ => StatusCodes.Status500InternalServerError
            };


            //change contentType




            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequest, ErrorToReturn response)
        {
            response.Errors = badRequest.Errors;
          return StatusCodes.Status400BadRequest;
        }

        private static async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatCode = StatusCodes.Status404NotFound,

                    ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(Response);

            }
        }
    }
}
