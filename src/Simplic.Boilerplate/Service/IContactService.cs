using System.Threading.Tasks;
using System;

namespace Simplic.Boilerplate
{
    public interface IContactService
    {
        Task<int> CreateAsync(Contact contact);
        Task<int> DeleteAsync(Contact contact);
        Task<int> DeleteAsync(Guid id);
        Task<int> UpdateAsync(Contact contact);
        Task<Contact> GetAsync(Guid id);
    }
}
