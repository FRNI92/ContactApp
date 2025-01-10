
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using ContactApp.Business.Services;
using Moq;

namespace ContactApp_Tests.Services_Tests;

public class ContactService_Tests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly IContactService _contactService;

    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IFileService _fileService;

    private readonly Mock<IContactFactory> _contactFactoryMock;
    private readonly IContactFactory _contactFactory;
    public ContactService_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _contactService = _contactServiceMock.Object;

        _fileServiceMock = new Mock<IFileService>();
        _fileService = _fileServiceMock.Object;

        _contactFactoryMock = new Mock<IContactFactory>();
        _contactFactory = _contactFactoryMock.Object;

    }

    [Fact]
    public void Create_Contact_ShouldSetCorrectValues()
    {
        //Arrrange, använder delvis mock av Icontactservice och delvis riktiga Contactservice
        var contact = new Contact { FirstName = "Fredrik", LastName = "Nilsson", Email = "Fred@domain.com",
            PhoneNumber = "123456789", StreetAdress = "Villavägen 12", PostalCode = "202 27",
            City = "Stockholm" };
        _contactServiceMock
            .Setup(cs => cs.Create())
            .Returns(contact);
        //Act
        var result = _contactService.Create();
        //Assert
        Assert.NotNull(result);
        Assert.Equal("Fredrik", result.FirstName);
        Assert.Equal("Nilsson", result.LastName);
        Assert.Equal("Fred@domain.com", result.Email);
        Assert.Equal("123456789", result.PhoneNumber);
        Assert.Equal("Villavägen 12", result.StreetAdress);
        Assert.Equal("202 27", result.PostalCode);
        Assert.Equal("Stockholm", result.City);
    }


    [Fact]
    public void Add_ShouldAddToList_AndSaveToFile()
    {
        //arrange
        var mockContact = new Contact { FirstName = "Fredrik", LastName = "Nilsson" };
        
        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns(new List<Contact> ());

        _contactFactoryMock.Setup(contactfactory => contactfactory.Create()).Returns(mockContact);

        var contactService = new ContactService(_contactFactoryMock.Object, _fileServiceMock.Object);
        //act
        contactService.Add(mockContact);
        //Assert
        _fileServiceMock.Verify(fs => fs.SaveToFile(It.IsAny<List<Contact>>()), Times.Once);
        var result = contactService.Read();
        Assert.Contains(mockContact, result);


        //var savedContact = result.FirstOrDefault(c => c.FirstName == "Fredrik" && c.LastName == "Nilsson");
        //Assert.NotNull(savedContact);
        //Assert.Equal("Fredrik", savedContact.FirstName);
        //Assert.Equal("Nilsson", savedContact.LastName);


    }

    [Fact]
    public void Delete_ShouldRemoveContact_AndSaveToFile()
    {
        //arrange
        var mockContact = new Contact { GuidId = Guid.NewGuid(), FirstName = "Fredrik", LastName = "Nilsson" };

        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns(new List<Contact> { mockContact });

        //_fileServiceMock.Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()));

        var contactService = new ContactService(_contactFactoryMock.Object, _fileServiceMock.Object);

        //act
        var resuult = contactService.Delete(mockContact.GuidId);
        //assert
        Assert.True(resuult);
        _fileServiceMock.Verify(fs => fs.SaveToFile(It.IsAny<List<Contact>>()), Times.Once);//kollar att savetofile anrpats

        var updatedList = contactService.Read();//läser in listan och dubbelkollar att mockCotact är borttage. laddar hela listan ifall flera objekt finns
        Assert.DoesNotContain(mockContact, updatedList);
    }

    [Fact]
    public void Read_ShouldLoadListFromFile_AndReturnList()
    {
        //arrange
        var mockContact = new Contact { FirstName = "Fredrik", LastName = "Nilsson" };

        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns(new List<Contact> { mockContact });

        var contactService  = new ContactService(_contactFactoryMock.Object, _fileServiceMock.Object);
        //act

        var result = contactService.Read();
        //assert
        Assert.Contains(mockContact, result.ToList());//konvertera IEnumerable till list så Asser.Contains fungerar
    }


    [Fact]
    public void Update_ShouldUpdateContactProperties_AndCallSaveToFile()
    {
        //arrange
        var firstMockContact = new Contact {GuidId = Guid.NewGuid(), FirstName = "Fredrik", LastName = "Nilsson" };
        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns(new List<Contact> { firstMockContact });//får en lilsta med contacten

        var updatedMockContact = new Contact { GuidId = firstMockContact.GuidId, FirstName = "Bert", LastName = "Josefsson" };
        _fileServiceMock.Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()));

        var contactService = new ContactService(_contactFactoryMock.Object, _fileServiceMock.Object);

        //act
        var result = contactService.Update(updatedMockContact);
        //assert
        Assert.True(result);
        Assert.Equal("Bert", firstMockContact.FirstName);
        Assert.Equal("Josefsson", firstMockContact.LastName);
        _fileServiceMock.Verify(fs => fs.SaveToFile(It.IsAny<List<Contact>>()), Times.Once);
    }
}


    //void Add(Contact contact);
    //Contact Create();
    //bool Delete(Guid id);
    //IEnumerable<Contact> Read();
    //bool Update(Contact contact);