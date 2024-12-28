using ContactApp.Business.Models;

namespace ContactApp.Business.Factories;

public class ContactFactory
{
    public Contact Create()
    {
        return new Contact
        {
            GuidId = Guid.NewGuid()
        };
    }
}
