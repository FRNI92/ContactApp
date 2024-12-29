using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
namespace ContactApp.Business.Factories;

public class ContactFactory : IContactFactory
{
    public Contact Create()
    {
        return new Contact
        {
            GuidId = Guid.NewGuid()
        };
    }
}
