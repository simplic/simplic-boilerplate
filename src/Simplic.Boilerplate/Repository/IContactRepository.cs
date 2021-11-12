using Simplic.Data;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IContactRepository
    {
        Task<int> CommitAsync();
        Task<Contact> GetAsync(Guid id);

        Task CreateAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Contact contact);
    }
}
