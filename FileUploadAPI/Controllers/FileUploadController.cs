using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUploadAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		private readonly ILogger<FileUploadController> _logger;

		public FileUploadController(ILogger<FileUploadController> logger)
		{
			_logger = logger;
		}
	}
}
