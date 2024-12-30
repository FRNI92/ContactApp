
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
namespace ContactApp.Business.Services;

public class ContactService : IContactService
{
    private List<Contact> _contacts = [];
    private readonly IContactFactory _contactFactory;
    private readonly IFileService _fileService;


    public ContactService(IContactFactory contactFactory, IFileService fileService)
    {
        _contactFactory = contactFactory;
        _fileService = fileService;

        _contacts = _fileService.LoadFromFile();
    }
    public Contact Create()
    {
        return _contactFactory.Create();
    }

    public void Add(Contact contact)
    {
        _contacts.Add(contact);
        _fileService.SaveToFile(_contacts);
    }

    public IEnumerable<Contact> Read()
    {
        _contacts = _fileService.LoadFromFile();
        return _contacts;
    }

    public bool Update(Contact contact)
    {
        var existingContact = _contacts.FirstOrDefault(c => c.GuidId == contact.GuidId);
        if (contact == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }
        return true;
    }

    public bool Delete(Guid id)
    {
        var contact = _contacts.FirstOrDefault(c => c.GuidId == id);
        if (contact == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }

        _contacts.Remove(contact); // Ta bort kontakten från listan
        _fileService.SaveToFile(_contacts); // Spara ändringarna till fil
        return true;
    }
}
