
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
        try
        {

        return _contactFactory.Create();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Occured During Create Phase: {ex.Message}");
            throw new InvalidOperationException("Was Not Able To Create Contact", ex);
        }
    }

    public void Add(Contact contact)
    {
        try
        {

        _contacts.Add(contact);
        _fileService.SaveToFile(_contacts);

        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error Occured When Adding contact{ex.Message}");
            throw new InvalidOperationException("Was Not Able To Add Contact Properly", ex);
        }
    }

    public IEnumerable<Contact> Read()
    {
        try
        {

        _contacts = _fileService.LoadFromFile();
        return _contacts;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Error Occured When Loading Contacts {ex.Message}");
            throw new InvalidOperationException("Was Not Able To Load File And Add To List", ex);
        }
    }

    public bool Update(Contact updatedContact)
    {
        try
        {
            var existingContact = _contacts.FirstOrDefault(c => c.GuidId == updatedContact.GuidId);
            if (existingContact == null)
            {
                Console.WriteLine("Contact not found in the list.");
                return false;
            }
            existingContact.FirstName = updatedContact.FirstName;
            existingContact.LastName = updatedContact.LastName;
            existingContact.Email = updatedContact.Email;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            existingContact.StreetAdress = updatedContact.StreetAdress;
            existingContact.PostalCode = updatedContact.PostalCode;
            existingContact.City = updatedContact.City;

            _fileService.SaveToFile(_contacts); 
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update resultet in error: {ex.Message}");
            return false;
        }

    }

    public bool Delete(Guid id)
    {
        try
        {
            var contact = _contacts.FirstOrDefault(contact => contact.GuidId == id);
            if (contact == null)
            {
                Console.WriteLine("User not found.");
                return false;
            }

            _contacts.Remove(contact); // Ta bort kontakten från listan
            _fileService.SaveToFile(_contacts); // Spara ändringarna till fil
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Error Occured when Deleting Contact: { ex.Message }");
            return false;
        }
    }
}
