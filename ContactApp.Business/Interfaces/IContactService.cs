using ContactApp.Business.Models;

namespace ContactApp.Business.Interfaces
{
    public interface IContactService
    {
        void Create(Contact contact);
        bool Delete(Guid id);
        IEnumerable<Contact> Read();
        bool Update(Guid id, string firstName, string lastName);
    }
}