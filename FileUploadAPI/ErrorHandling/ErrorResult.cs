using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FileUploadAPI.ErrorHandling
{
	public class ErrorResult : IErrorResult
	{
		private const string BadRequestTitle = "Bad request";
		private const string ValidationMessage = "Validation failed";
		private const string NotFoundTitle = "Not found";
		private const string InternalServerErrorTitle = "Internal server error";
		
		public BadRequestObjectResult BadRequest(string message)
		{
			return new BadRequestObjectResult(message);
		}

		public BadRequestObjectResult BadRequest(ModelStateDictionary modelState, string message = null)
		{
			return new BadRequestObjectResult(new ValidationProblemDetails(modelState)
			{
				Status = (int)HttpStatusCode.BadRequest,
				Title = BadRequestTitle,
				Detail = message ?? ValidationMessage
			});
		}

		public NotFoundObjectResult NotFound(string message)
		{
			return new NotFoundObjectResult(new ProblemDetails
			{
				Status = (int)HttpStatusCode.NotFound,
				Title = NotFoundTitle,
				Detail = message
			});
		}

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

		public ConflictObjectResult Conflict(string message)
		{
			return Conflict(message);
		}

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
