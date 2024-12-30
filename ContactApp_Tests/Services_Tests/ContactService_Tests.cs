
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using ContactApp.Business.Services;
using Moq;

namespace ContactApp_Tests.Services_Tests;

public class ContactService_Tests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _contactService = _contactServiceMock.Object;
    }

    [Fact]
    public void Create_Contact_ShouldSetCorrectValues()
    {
        //Arrrange
        var contact = new Contact { FirstName = "Fredrik", LastName = "Nilsson", Email = "Fred@domain.com",
                                    PhoneNumber = "123456789", StreetAdress = "Villavägen 12", PostalCode = "202 27",
                                    City = "Stockholm"}; 
        _contactServiceMock
            .Setup(cs => cs.Create())
            .Returns(contact);
        //Act
        var result =_contactService.Create();
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
}


    //void Add(Contact contact);
    //Contact Create();
    //bool Delete(Guid id);
    //IEnumerable<Contact> Read();
    //bool Update(Contact contact);