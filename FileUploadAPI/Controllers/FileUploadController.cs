using FileUploadAPI.ErrorHandling;
using FileUploadAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUploadAPI.Controllers
{
	[Route("api")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		private readonly IFileManager _fileManager;
		private readonly IErrorResult _errorResult;

		private const string InvalidStateMessage = "Invalid state.";
		private const string FileRequiredMessage = "File is required.";
		private const string InvalidFileFormatMessage = "Invalid format. XML file is required.";
		private const string ConvertFileErrorMessage = "An error occured in file converting.";
		private const string ConvertFileErrorTitle = "Convert file error";
		private const string FileNotCreatedMessage = "An error occured. File not created.";
		private const string SaveFileErrorTitle = "Save file error";

		public FileUploadController(IFileManager fileManager, IErrorResult errorResult)
		{
			_fileManager = fileManager;
			_errorResult = errorResult;
		}

		/// <summary>
		/// Uploads file in a specified directory.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> UploadFileAsync(IFormFile file)
		{
			if (!ModelState.IsValid)
				return _errorResult.BadRequest(ModelState, InvalidStateMessage);

			if (file == null)
				return _errorResult.BadRequest(FileRequiredMessage);

			if (!_fileManager.ValidateXmlFile(file.FileName))
				return _errorResult.BadRequest(InvalidFileFormatMessage);

			var jsonResult = _fileManager.ConvertXmlFileToJson(file);
			
			if (string.IsNullOrEmpty(jsonResult))
				return _errorResult.CustomError(HttpStatusCode.BadRequest, ConvertFileErrorTitle, ConvertFileErrorMessage);

			var result = await _fileManager.SaveFileAsync(file.FileName, jsonResult);
			
			if (string.IsNullOrEmpty(result))
				return _errorResult.CustomError(HttpStatusCode.BadRequest, SaveFileErrorTitle, FileNotCreatedMessage);

			return Ok(result);
		}

		/// <summary>
		/// Uploads multiple files
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("uploads")]
		public async Task<IActionResult> UploadMultipleFilesAsync(IList<IFormFile> files)
		{
			if (!ModelState.IsValid)
				return _errorResult.BadRequest(ModelState, InvalidStateMessage);
		
			if (files.Count == 0)
				return _errorResult.BadRequest(FileRequiredMessage);
			
			foreach (var file in files)
			{
				if (!_fileManager.ValidateXmlFile(file.FileName))
					return _errorResult.BadRequest(InvalidFileFormatMessage);

				var jsonResult = _fileManager.ConvertXmlFileToJson(file);

				if (string.IsNullOrEmpty(jsonResult))
					return _errorResult.CustomError(HttpStatusCode.BadRequest, ConvertFileErrorTitle, ConvertFileErrorMessage);

				var result = await _fileManager.SaveFileAsync(file.FileName, jsonResult);

				if (string.IsNullOrEmpty(result))
					return _errorResult.CustomError(HttpStatusCode.BadRequest, SaveFileErrorTitle, FileNotCreatedMessage);
			}

			return Ok();
		}
	}
}
