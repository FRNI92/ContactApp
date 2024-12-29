using ContactApp.Business.Models;

namespace ContactApp.Business.Interfaces
{
    public interface IContactService
    {
        void Add(Contact contact);
        Contact Create();
        bool Delete(Guid id);
        IEnumerable<Contact> Read();
        bool Update(Guid id, string firstName, string lastName);
    }
}