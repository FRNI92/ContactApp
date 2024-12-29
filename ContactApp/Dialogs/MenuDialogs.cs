using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using static ContactApp.Dialogs.MenuDialogs;

namespace ContactApp.Dialogs;

public class MenuDialogs
{
 
    private readonly IContactService _contactService;
    private readonly IFileService _fileService;
    public MenuDialogs(IContactService contactService, IFileService fileService) =>
        (_contactService, _fileService) = (contactService, fileService);

    public void RunMenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\tCONTACT-MANAGER");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("\tMainMenu");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("\tChoose An Option");
            Console.WriteLine("\t[1]. Create Contact");
            Console.WriteLine("\t[2]. Show Contact List");
            Console.WriteLine("\t[3]. Update Contact List");
            Console.WriteLine("\t[4]. EXIT\n");
            Console.Write("Enter Your Option:");

            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    CreateUserOption();
                    break;

                case "2":
                    ShowListOption();
                    break;
                    
                case "3":
                    UpdateUser();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    public void CreateUserOption()
    {

        Contact contact = _contactService.Create();

        Console.Clear();
        Console.WriteLine("\tCONTACT-MANAGER");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("\tCreating A User");
        Console.WriteLine("\tPlease Fill Out This Form");

        contact.FirstName = CheckingNull("Enter First Name: ");
        contact.LastName = CheckingNull("Enter Last Name: ");
        contact.Email = CheckingNull("Enter Email:");
        contact.PhoneNumber = CheckingNull("Enter PhoneNumber: ");
        contact.StreetAdress = CheckingNull("Enter StreetAdress: ");
        contact.PostalCode = CheckingNull("Enter PostalCode: ");
        contact.City = CheckingNull("Enter City: ");


        Console.WriteLine("Would You Like To Save Data To File?: Y/N");
        string? userChoice = Console.ReadLine()?.Trim().ToLower();

        if (userChoice == "y")
        {
            _contactService.Add(contact);
            Console.WriteLine("Contact Saved To File Successfully!");
            Console.ReadKey();
        }
        else if (userChoice == "n")
        {
            Console.WriteLine("Contact Not Saved");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Invalid Input. Please Enter Y or N.");
        }

    }

    public void ShowListOption()
    {
        Console.Clear();
        Console.WriteLine("\tCONTACT-MANAGER");
        Console.WriteLine("\tContact List");

        Console.WriteLine("This Is The Contact List:");
        var contact = _fileService.LoadFromFile();
        foreach (var contacts in contact)
        {
            Console.WriteLine($"Name: {contacts.FirstName} {contacts.LastName}");
            Console.WriteLine($"Email: {contacts.Email}");
            Console.WriteLine($"Phone: {contacts.PhoneNumber}");
            Console.WriteLine($"Street: {contacts.StreetAdress}");
            Console.WriteLine($"Postal Code: {contacts.PostalCode}");
            Console.WriteLine($"City: {contacts.City}");
            Console.WriteLine("------------------------------------");
        }
        Console.WriteLine("----Press Any Key To Go Back To Menu");
        Console.ReadKey();
    }


    public void UpdateUser()
    {
        Console.Clear();
        Console.WriteLine("\tCONTACT-MANAGER");
        Console.WriteLine("\tUpdate Contact List");

        Console.WriteLine("This Is The Contact List:");
        var contact = _fileService.LoadFromFile();

        int index = 1;
        foreach (var contacts in contact)
        {
            Console.WriteLine($"{index}.Name: {contacts.FirstName} {contacts.LastName}");
            Console.WriteLine($"Email: {contacts.Email}");
            Console.WriteLine($"Phone: {contacts.PhoneNumber}");
            Console.WriteLine($"Street: {contacts.StreetAdress}");
            Console.WriteLine($"Postal Code: {contacts.PostalCode}");
            Console.WriteLine($"City: {contacts.City}");
            Console.WriteLine("------------------------------------");
            index++;
        }
        Console.WriteLine("----Witch Contact Would You Like To Update");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= contact.Count)
        {
            var selectedContact = contact[choice - 1];
            Console.WriteLine($"Updating contact: {selectedContact.FirstName} {selectedContact.LastName}");
            selectedContact.FirstName = CheckingNull($"Current First Name ({selectedContact.FirstName}): ");
            selectedContact.LastName = CheckingNull($"Current Last Name ({selectedContact.LastName}): ");
            selectedContact.Email = CheckingNull($"Current Email ({selectedContact.Email}): ");
            selectedContact.PhoneNumber = CheckingNull($"Current Phone Number ({selectedContact.PhoneNumber}): ");
            selectedContact.StreetAdress = CheckingNull($"Current Street Address ({selectedContact.StreetAdress}): ");
            selectedContact.PostalCode = CheckingNull($"Current Postal Code ({selectedContact.PostalCode}): ");
            selectedContact.City = CheckingNull($"Current City ({selectedContact.City}): ");
            _fileService.SaveToFile(contact);
            Console.WriteLine("Contact updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
        Console.ReadKey();
    }



    private string CheckingNull(string textFromUser)//för att städa upp lite
    {
        Console.Write(textFromUser);
        string? input = Console.ReadLine()?.Trim();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be empty. Please try again.");
            Console.Write(textFromUser);
            input = Console.ReadLine()?.Trim();

        }

        return input!;
    }
}

