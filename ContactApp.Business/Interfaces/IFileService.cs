using ContactApp.Business.Models;

namespace ContactApp.Business.Interfaces
{
    public interface IFileService
    {
        List<Contact> LoadFromFile();
        void SaveToFile(List<Contact> listIn);
    }
}