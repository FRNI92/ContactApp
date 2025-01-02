using ContactApp.Business.Factories;

namespace ContactApp_Tests.Factories_Tests;

public class ContactFactory_Tests
{
    [Fact]
    public void create_ShouldTakeInput_CreateContact_WithGuid()
    {
        //arrange
        var factory = new ContactFactory();

        //act
        var contact = factory.Create();
        contact.FirstName = "Fredrik";
        contact.LastName = "Nilsson";
        contact.Email = "Fredrik@domain.com";
        contact.PhoneNumber = "058110131";
        contact.StreetAdress = "Basketorp 713";
        contact.PostalCode = "718 95";
        contact.City = "Ervalla";

        //assert
        Assert.Equal("Fredrik", contact.FirstName);
        Assert.Equal("Nilsson", contact.LastName); 
        Assert.Equal("Fredrik@domain.com", contact.Email);
        Assert.Equal("058110131", contact.PhoneNumber);
        Assert.Equal("Basketorp 713", contact.StreetAdress);
        Assert.Equal("718 95", contact.PostalCode);
        Assert.Equal("Ervalla", contact.City);

        Assert.NotEqual(Guid.Empty, contact.GuidId);
    }
}
