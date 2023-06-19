namespace FileUploadAPI.Utils
{
	public interface IFileManager
	{
		Task<string> SaveFileAsync(string fileName, string jsonResult);
		bool ValidateXmlFile(string fileName);
		string ConvertXmlFileToJson(IFormFile file);
	}
}
