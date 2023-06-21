using FileUploadAPI.Controllers;
using FileUploadAPI.ErrorHandling;
using FileUploadAPI.Utils;
using FileUploadApiTests.TestData;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;

namespace FileUploadTests
{
	public class FileUploadTests
	{
		private IFileManager _fileManager;
		private IErrorResult _errorResult;

		public FileUploadTests()
		{
			_fileManager = new Mock<IFileManager>().Object;
			_errorResult = new Mock<IErrorResult>().Object;
		}

		[Fact]
		public async Task Should_Upload_File()
		{
			//// Arrange
			var bytes = Encoding.UTF8.GetBytes(TestData.XmlContent);
			IFormFile file = CreateTemporaryFormFile("Dummy.xml", bytes, "text/xml");
			var sut = new FileUploadController(_fileManager, _errorResult);

			//// Act
			var result = (OkObjectResult)await sut.UploadFileAsync(file);

			//// Assert
			Assert.NotNull(result);
			result.StatusCode.Should().Be(200);
		}

		[Fact]
		public async Task ShouldFail_WhenUploadEmptyFile()
		{
			// Arrange
			IFormFile file = null;
			var sut = new FileUploadController(_fileManager, _errorResult);

			// Act
			var result = (BadRequestObjectResult)await sut.UploadFileAsync(file);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
			result.StatusCode.Should().Be(400);
		}

		[Fact]
		public void ShouldFail_WhenFileIsNot_Xml()
		{
			// Arrange
			var bytes = Encoding.UTF8.GetBytes(TestData.TextContent);
			IFormFile file = CreateTemporaryFormFile("Dummy.txt", bytes, "text/plain");
			
			// Act
			var result = _fileManager.ValidateXmlFile(file.FileName);
			
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Should_Convert_Xml_ToJson()
		{
			// Arrange
			var bytes = Encoding.UTF8.GetBytes(TestData.XmlContent);
			IFormFile file = CreateTemporaryFormFile("Dummy.xml", bytes, "text/xml");

			// Act
			var result = _fileManager.ConvertXmlFileToJson(file);

			// Assert
			Assert.IsType<string>(result);
		}


		private IFormFile CreateTemporaryFormFile(string fileName, byte[] bytes, string contentType)
		{
			var formFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "file", fileName) { Headers = new HeaderDictionary() };

			formFile.ContentType = contentType;
			formFile.ContentDisposition = $"form-data; name=\"file\"; filename=\"{fileName}\"";

			return formFile;
		}
	}
}