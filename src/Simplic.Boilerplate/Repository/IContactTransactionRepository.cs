using Simplic.Data;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IContactTransactionRepository
    {
        Task CreateAsync(Contact contact, ITransaction transaction);
        Task DeleteAsync(Guid id, ITransaction transaction);
        Task UpdateAsync(Contact contact, ITransaction transaction);
    }
}
