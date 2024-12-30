
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
        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns(new List<Contact>());//ville inte ändra koden jag hade så jag instansierar listan såhär
        
        var mockContact = new Contact { FirstName = "Fredrik", LastName = "Nilsson" };

        _contactFactoryMock.Setup(contactfactory => contactfactory.Create()).Returns(mockContact);

        var contactService = new ContactService(_contactFactoryMock.Object, _fileServiceMock.Object);
        //act
        contactService.Add(mockContact);
        //Assert
        _fileServiceMock.Verify(fs => fs.SaveToFile(It.IsAny<List<Contact>>()), Times.Once);//dubbelkollar att den metoden användes
        var result = contactService.Read();// kollar att det ligger någon i listan
        Assert.Contains(mockContact, result);


        //var savedContact = result.FirstOrDefault(c => c.FirstName == "Fredrik" && c.LastName == "Nilsson");
        //Assert.NotNull(savedContact);
        //Assert.Equal("Fredrik", savedContact.FirstName);
        //Assert.Equal("Nilsson", savedContact.LastName);


    }

    [Fact]
    pu
}


    //void Add(Contact contact);
    //Contact Create();
    //bool Delete(Guid id);
    //IEnumerable<Contact> Read();
    //bool Update(Contact contact);