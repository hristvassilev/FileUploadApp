using Newtonsoft.Json;
using System.Xml;

namespace FileUploadAPI.Utils
{
	/// <summary>
	/// Implements file related actions.
	/// </summary>
	public class FileManager : IFileManager
	{
		private readonly ILogger<FileManager> _logger;

		private const string FileConvertError = "File convert error: ";
		private const string SaveFileError = "Write file error: ";
		private const string UploadsFolder = "Uploads";
		private const string XmlExtension = ".xml";
		private const string JsonExtension = ".json";

		public FileManager(ILogger<FileManager> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Creates and save a new json file async.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="jsonResult"></param>
		/// <returns></returns>
		public async Task<string> SaveFileAsync(string fileName, string jsonResult)
		{
			try
			{
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolder);

				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				fileName = string.Concat(Path.GetFileNameWithoutExtension(fileName), JsonExtension);
				var filePath = Path.Combine(uploadsFolder, fileName);
				await File.WriteAllTextAsync(filePath, jsonResult);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, SaveFileError + ex.Message);
				return string.Empty;
			}
			
			return fileName;
		}

		/// <summary>
		/// Validates if file format is XML (required).
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public bool ValidateXmlFile(string fileName) =>  Path.GetExtension(fileName).ToLowerInvariant() == XmlExtension;

		/// <summary>
		/// Converts XML file to Json string.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public string ConvertXmlFileToJson(IFormFile file)
		{
			var jsonResult = string.Empty;

			try
			{
				using (StreamReader reader = new StreamReader(file.OpenReadStream()))
				{
					var xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(reader.ReadToEnd());
					jsonResult = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, FileConvertError + ex.Message);
			}

			return jsonResult;
		}
	}
}
