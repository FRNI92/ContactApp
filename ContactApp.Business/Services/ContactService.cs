﻿
using ContactApp.Business.Models;
namespace ContactApp.Business.Services;

public class ContactService
{
    private List<Contact> _contacts = [];
    private readonly FileService _fileService = new();


    public void Create(Contact contact)
    {
        _contacts.Add(contact);
        _fileService.SaveToFile(_contacts);
    }

    public IEnumerable<Contact> Read()
    {
        _contacts = _fileService.LoadFromFile();
        return _contacts;
    }

    public bool Update(Guid id, string firstName, string lastName)
    {
        var contact = _contacts.FirstOrDefault(c => c.GuidId == id);
        if (contact == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }

        contact.FirstName = firstName;
        contact.LastName = lastName;

        Console.WriteLine("User updated successfully.");
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

        _contacts.Remove(contact);
        Console.WriteLine("User deleted successfully.");
        return true;
    }
}
