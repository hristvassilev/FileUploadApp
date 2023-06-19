using FileUploadAPI.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUploadAPI.Controllers
{
	[Route("api")]
	[ApiController]
	public class FileUploadController : BaseApiController
	{
		private readonly IFileManager _fileManager;

		private const string InvalidStateMessage = "Invalid state.";
		private const string FileRequiredMessage = "File is required.";
		private const string InvalidFileFormatMessage = "Invalid format. XML file is required.";
		private const string ConvertFileErrorMessage = "Convert file error!";
		private const string FileNotCreatedMessage = "File not created!";

		public FileUploadController(IFileManager fileManager)
		{
			_fileManager = fileManager;
		}

		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> UploadFileAsync(IFormFile file)
		{
			if (!ModelState.IsValid)
				return ErrorResult.BadRequest(ModelState, InvalidStateMessage);

			if (file == null)
				return ErrorResult.BadRequest(FileRequiredMessage);

			if (!_fileManager.ValidateXmlFile(file.FileName))
				return ErrorResult.BadRequest(InvalidFileFormatMessage);

			var jsonResult = _fileManager.ConvertXmlFileToJson(file);
			
			if (string.IsNullOrEmpty(jsonResult))
				return ErrorResult.BadRequest(ConvertFileErrorMessage);

			var result = await _fileManager.SaveFileAsync(file.FileName, jsonResult);
			
			if (string.IsNullOrEmpty(jsonResult))
				return ErrorResult.BadRequest(FileNotCreatedMessage);

			return Ok(result);
		}

		[HttpPost]
		[Route("uploads")]
		public async Task<IActionResult> UploadMultipleFilesAsync(IList<IFormFile> files)
		{
			//if (!ModelState.IsValid)
			//{
			//	return ErrorResult.BadRequest(ModelState, InvalidStateMessage);
			//}

			//if (files.Count == 0)
			//{
			//	return ErrorResult.BadRequest(FileRequiredMessage);
			//}

			//foreach (var file in files)
			//{
			//	await _fileManager.SaveFileAsync(file);
			//}
			
			return Ok();
		}
	}
}
