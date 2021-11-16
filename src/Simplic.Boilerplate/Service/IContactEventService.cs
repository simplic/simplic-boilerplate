using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Interface for
    /// </summary>
    public interface IContactEventService
    {
        /// <summary>
        /// Asynchronously sends a created event.
        /// </summary>
        /// <param name="contact">Contact that was created.</param>
        /// <returns>Task.</returns>
        Task SendCreatedEventAsync(Contact contact);

        /// <summary>
        /// Asynchronously sends a update event.
        /// </summary>
        /// <param name="contact">Contact that was updated.</param>
        /// <returns>Task.</returns>
        Task SendUpdatedEventAsync(Contact contact);

        /// <summary>
        /// Asynchronously sends a deletion event.
        /// </summary>
        /// <param name="contactId">Identifier of contact that was deleted.</param>
        /// <returns>Task.</returns>
        Task SendDeletedEventAsync(Guid contactId);
    }
}
