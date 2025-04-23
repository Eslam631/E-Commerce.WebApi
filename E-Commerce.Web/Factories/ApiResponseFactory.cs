using Microsoft.AspNetCore.Mvc;
using Shared.ErroeModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
            
                var Errors = Context.ModelState.Where(M => M.Value.Errors.Any()).Select(E => new ValidationError()
                {
                    Falied = E.Key,
                    Errors = E.Value.Errors.Select(R => R.ErrorMessage)

                });

                var Response = new ValidationErrorToReturn()
                {
                    validationErrors = Errors
                };

                return new BadRequestObjectResult(Response);


            
            }
    }
}
