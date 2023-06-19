using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FileUploadAPI.ErrorHandling
{
	public class ErrorResult : IErrorResult
	{
		private const string BadRequestTitle = "Bad request.";
		private const string ValidationMessage = "Validation failed.";
		private const string NotFoundTitle = "Not found.";
		private const string InternalServerErrorTitle = "Internal server error.";
		
		/// <summary>
		/// Returns BadRequest result with custom message.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public BadRequestObjectResult BadRequest(string message)
		{
			return new BadRequestObjectResult(message);
		}

		/// <summary>
		/// Returns BadRequest result with status, title and detail.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public BadRequestObjectResult BadRequest(ModelStateDictionary modelState, string message = null)
		{
			return new BadRequestObjectResult(new ValidationProblemDetails(modelState)
			{
				Status = (int)HttpStatusCode.BadRequest,
				Title = BadRequestTitle,
				Detail = message ?? ValidationMessage
			});
		}

		/// <summary>
		/// Returns NotFound result with custom message.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public NotFoundObjectResult NotFound(string message)
		{
			return new NotFoundObjectResult(new ProblemDetails
			{
				Status = (int)HttpStatusCode.NotFound,
				Title = NotFoundTitle,
				Detail = message
			});
		}

		/// <summary>
		/// Returns Internal server error result with custom message.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public ObjectResult InternalServerError(string message)
		{
			var problem = new ProblemDetails
			{
				Status = (int)HttpStatusCode.InternalServerError,
				Title = InternalServerErrorTitle,
				Detail = message
			};

			var result = new ObjectResult(problem);
			result.StatusCode = problem.Status;

			return result;
		}

		/// <summary>
		/// Returns custom Object result with status code, title and message.
		/// </summary>
		/// <param name="statusCode"></param>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public ObjectResult CustomError(HttpStatusCode statusCode, string title, string message)
		{
			return new ObjectResult(new ProblemDetails
			{
				Status = (int)statusCode,
				Title = title,
				Detail = message
			});
		}
	}
}
