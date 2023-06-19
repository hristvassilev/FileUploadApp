using FileUploadAPI.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAPI.Controllers
{
    public abstract class BaseApiController : ControllerBase
	{
		private ILogger _logger;
		private IErrorResult _errorResult;

		protected ILogger Logger 
		{
			get
			{
				if (_logger == null)
				{
					ILoggerFactory loggerFactory = HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
					_logger = loggerFactory.CreateLogger(GetType());
				}

				return _logger;
			}
		}

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
