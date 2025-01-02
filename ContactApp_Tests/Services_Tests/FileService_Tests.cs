﻿using ContactApp.Business.Interfaces;
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
        File.WriteAllText(filePath, "[{ Invalid file]");// use file below to fail test
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

    [Fact]
    public void SaveToFile_ShouldConvertAndSaveToFile()
    {
        //arrange
        var testDirectory = "TestDataFile";
        var testFileName = "testFile.json";
        IFileService fileService = new FileService(testDirectory, testFileName);

        var testContact = new List<Contact>
        {
            new Contact { FirstName = "Fredrik", LastName = "Nilsson", Email = "Fredrik@domain.com" }
        };
        //act
        fileService.SaveToFile(testContact);
        //assert
        var filePath = Path.Combine(testDirectory, testFileName);
        Assert.True(File.Exists(filePath));

        var ContactInFile = File.ReadAllText(filePath);
        Assert.Contains("Fredrik", ContactInFile);
        Assert.Contains("Nilsson", ContactInFile);
        Assert.Contains("Fredrik@domain.com", ContactInFile);
    }



}



//SaveToFile
//LoadFromFile
//Check load from file catch