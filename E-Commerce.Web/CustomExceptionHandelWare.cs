using E_Commerce.Web.ErroeModels;

namespace E_Commerce.Web
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
            }
            catch (Exception ex)
            {
                //
                _logger.LogError(ex, "SomeThing Went Wrong");

                //Change StateCode

                httpContext.Response.StatusCode=StatusCodes.Status500InternalServerError;

                //change contentType

                httpContext.Response.ContentType="application/json";

                var Response = new ErrorToReturn()
                {
                    StatCode = StatusCodes.Status500InternalServerError,
                     ErrorMessage = ex.Message,
                };

                await httpContext.Response.WriteAsJsonAsync( Response );
                
            }
        }
    }
}
