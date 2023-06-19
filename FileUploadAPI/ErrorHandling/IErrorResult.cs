using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FileUploadAPI.ErrorHandling
{
    public interface IErrorResult
    {
        BadRequestObjectResult BadRequest(string message);
        BadRequestObjectResult BadRequest(ModelStateDictionary modelState, string message = null);
        NotFoundObjectResult NotFound(string message);
        ObjectResult InternalServerError(string message);
        ConflictObjectResult Conflict(string message);
        ObjectResult CustomError(HttpStatusCode statusCode, string title, string message);
    }
}
