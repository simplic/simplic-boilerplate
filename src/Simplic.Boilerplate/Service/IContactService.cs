using Simplic.Data;
using Simplic.Data.NoSql;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Service for managing contact directly  and contact via transactions.
    /// </summary>
    public interface IContactService : ITransactionRepository<Contact, Guid>, IRepository<Guid, Contact, ContactFilter>
    {
        /// <summary>
        /// Asynchronously deletes a contact object and sends a deleted event.
        /// </summary>
        /// <param name="contact">Contact to delete.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Contact contact);
    }
}
