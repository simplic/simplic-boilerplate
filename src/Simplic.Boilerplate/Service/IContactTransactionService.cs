using System.Threading.Tasks;
using System;
using Simplic.Data;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Service for managing contact transactions.
    /// </summary>
    public interface IContactTransactionService : IContactTransactionRepository
    {
        /// <summary>
        /// Asynchronously creates a new transaction.
        /// </summary>
        /// <returns>Task of transaction.</returns>
        Task<ITransaction> CreateTransactionAsync();

        /// <summary>
        /// Asynchronously commits a transaction.
        /// </summary>
        /// <param name="transaction">Transaction to commit.</param>
        /// <returns>Task.</returns>
        Task CommitAsync(ITransaction transaction);

        /// <summary>
        /// Asynchronously aborts a transaction.
        /// </summary>
        /// <param name="transaction">Transaction to abort.</param>
        /// <returns>Task.</returns>
        Task AbortAsync(ITransaction transaction);


        /// <summary>
        /// Asynchronously adds deletion of a contact object to the transaction.
        /// <para> Adds a deletion event to the event transaction.</para>
        /// </summary>
        /// <param name="contact">Contact to delete.</param>
        /// <param name="transaction">Transaction to add to.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Contact contact, ITransaction transaction);
    }
}
