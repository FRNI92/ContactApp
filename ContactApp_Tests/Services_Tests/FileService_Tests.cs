using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using ContactApp.Business.Services;
using Moq;
namespace ContactApp_Tests.Services_Tests;

public class FileService_Tests
{
    private readonly IFileService _fileService;
    private readonly Mock<IFileService> _fileServiceMock;
    public FileService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _fileService = _fileServiceMock.Object;
    }
    [Fact]
    public void LoadFromFile_ShouldReturnEmptyList_WhenExceptionIsThrown()
    {
        //arrange
        var testDirectory = "TestDataFile";
        var testFileName = "damagedFile.json";

        IFileService fileService = new FileService(testDirectory, testFileName);

        if (!Directory.Exists(testDirectory))
        {
            Directory.CreateDirectory(testDirectory);
        }
        var filePath = Path.Combine(testDirectory, testFileName);
        File.WriteAllText(filePath, "[{ \"invalidjson\": \"value\" }]");// use file below
        //correct file, will return file and fail test: [{ \"invalidjson\": \"value\" }]

        //act
        var result = fileService.LoadFromFile();
        //assert
        Assert.NotNull(result);
        Assert.Empty(result);

        //if (Directory.Exists(testDirectory))
        //{
        //    Directory.Delete(testDirectory, true);
        //}
    }

}



//SaveToFile
//LoadFromFile
//Check load from file catch