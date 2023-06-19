using FileUploadAPI.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAPI.Controllers
{
	/// <summary>
	/// Abstract class that inherited ControllerBase.
	/// </summary>
    public abstract class BaseApiController : ControllerBase
	{
		private IErrorResult _errorResult;

		protected IErrorResult ErrorResult
		{
			get
			{
				if (_errorResult == null)
				{
					_errorResult = HttpContext.RequestServices.GetRequiredService<IErrorResult>();
				}

				return _errorResult;
			}
		}
	}
}
