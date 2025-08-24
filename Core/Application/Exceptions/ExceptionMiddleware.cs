using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using ValidationException = FluentValidation.ValidationException;

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

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			int statusCode = GetStatusCode(exception);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			if(exception.GetType() == typeof(ValidationException))
			{
				await context.Response.WriteAsync(new ExceptionModel
				{
					StatusCode = StatusCodes.Status400BadRequest,
					Errors = ((ValidationException)exception).Errors.Select(e => e.ErrorMessage)
				}.ToString());
				return;
			}	

			List<string> errors = new()
			{
				exception.Message,
				exception.InnerException?.ToString()
			};

			await context.Response.WriteAsync(new ExceptionModel
			{
				Errors = errors,
				StatusCode = statusCode
			}.ToString());

		}

		private static int GetStatusCode(Exception exception) 
		{
			return exception switch
			{
				BadRequestException => StatusCodes.Status400BadRequest,
				NotFoundException => StatusCodes.Status400BadRequest,
				ValidationException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};
		}
	}
}
