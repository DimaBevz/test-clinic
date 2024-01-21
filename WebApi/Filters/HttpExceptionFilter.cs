using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

namespace WebApi.Filters
{
    public class HttpExceptionFilter : IExceptionFilter
    {
        public HttpExceptionFilter() { }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception exception)
            {
                HandleException(context, exception, StatusCodes.Status400BadRequest, "Something went wrong!");
            }
            else if (context.Exception is DbUpdateException dbException)
            {
                HandleException(context, dbException, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        private static void HandleException<T>(ExceptionContext context, T exception, int statusCode, string message) 
            where T : Exception
        {
            var details = new ValidationProblemDetails
            {
                Instance = context.HttpContext.Request.Path,
                Detail = exception.StackTrace,
                Status = statusCode,
                Title = exception.Message
            };

            var response = new ApiResponse<ValidationProblemDetails>
            {
                IsSuccess = false,
                Message = message,
                Data = details
            };

            context.Result = new ObjectResult(response) 
            {
                StatusCode = statusCode
            };

            context.HttpContext.Response.StatusCode = statusCode;
            context.ExceptionHandled = true;
        }
    }
}
