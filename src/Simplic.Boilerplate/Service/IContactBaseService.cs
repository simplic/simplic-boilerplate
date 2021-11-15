using System.Threading.Tasks;
using System;

namespace Simplic.Boilerplate
{
    public interface IContactBaseService
    {
        Task<Contact> GetAsync(Guid id);

        Task CreateAsync(Contact contact);
        Task DeleteAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Contact contact);
    }
}
