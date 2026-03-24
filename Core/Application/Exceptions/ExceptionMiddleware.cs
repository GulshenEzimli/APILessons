using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using DataValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }


        private async static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = GetStatusCode(exception); 
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            if (exception is ValidationException validationException)
            {
                var model = new ExceptionModel()
                {
                    Errors = validationException.Errors.Select(e => e.ErrorMessage),
                    StatusCode = statusCode
                };

                await context.Response.WriteAsync(model.ToString());
                return;
            }

            List<string> errors = new ()
            {
                exception.Message,
                exception.InnerException?.ToString()
            };

            ExceptionModel exceptionModel = new()
            {
                Errors = errors,
                StatusCode = statusCode
            };

            await context.Response.WriteAsync(exceptionModel.ToString());
        }


        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status400BadRequest,
                DataValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
