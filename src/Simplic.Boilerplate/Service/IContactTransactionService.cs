using System.Threading.Tasks;
using System;
using Simplic.Data;

namespace Simplic.Boilerplate
{
    public interface IContactTransactionService
    {
        Task<ITransaction> CreateTransactionAsync();
        Task CommitAsync(ITransaction transaction);
        Task AbortAsync(ITransaction transaction);

        Task CreateAsync(Contact contact, ITransaction transaction);
        Task DeleteAsync(Contact contact, ITransaction transaction);
        Task DeleteAsync(Guid id, ITransaction transaction);
        Task UpdateAsync(Contact contact, ITransaction transaction);
    }
}
