using Simplic.Data;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Repository for managing contact transactions.
    /// </summary>
    public interface IContactTransactionRepository
    {
        /// <summary>
        /// Asynchronously adds creation of a new contact object to the transaction.
        /// <para> Adds a created event to the event transaction.</para>
        /// </summary>
        /// <param name="contact">Contact to create.</param>
        /// <param name="transaction">Transaction to add to.</param>
        /// <returns>Task.</returns>
        Task CreateAsync(Contact contact, ITransaction transaction);

        /// <summary>
        /// Asynchronously adds deletion of a contact object to the transaction.
        /// <para> Adds a deletion event to the event transaction.</para>
        /// </summary>
        /// <param name="id">Identifier of contact to delete.</param>
        /// <param name="transaction">Transaction to add to.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Guid id, ITransaction transaction);

        /// <summary>
        /// Asynchronously adds update of a contact object to the transaction.
        /// <para> Adds a update event to the event transaction.</para>
        /// </summary>
        /// <param name="contact">Contact to update.</param>
        /// <param name="transaction">Transaction to add to.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(Contact contact, ITransaction transaction);
    }
}
