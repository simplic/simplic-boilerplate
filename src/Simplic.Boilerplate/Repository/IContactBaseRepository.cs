using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IContactBaseRepository
    {
        Task<int> CommitAsync();
        Task<Contact> GetAsync(Guid id);

        Task CreateAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Contact contact);
    }
}
