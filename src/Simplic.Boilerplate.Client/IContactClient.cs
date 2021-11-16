using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    /// <summary>
    /// Interface for the contact client.
    /// </summary>
    public interface IContactClient
    {
        /// <summary>
        /// Asynchronously retrieves a contact.
        /// </summary>
        /// <param name="id">Identifier of the contact.</param>
        /// <returns>Task of the contact model.</returns>
        Task<ContactModel> GetAsync(Guid id);

        /// <summary>
        /// Asynchronously creates a contact.
        /// </summary>
        /// <param name="model">Model of the contact to create.</param>
        /// <returns>Task of the created contact model.</returns>
        Task<ContactModel> CreateAsync(CreateContactRequest model);

        /// <summary>
        /// Asynchronously updates a contact.
        /// </summary>
        /// <param name="model">Model of the contact to update.</param>
        /// <returns>Task of the updated contact model.</returns>
        Task<ContactModel> UpdateAsync(UpdateContactRequest model);

        /// <summary>
        /// Asynchronously deletes a contact.
        /// </summary>
        /// <param name="id">Identifier of the contact to delete.</param>
        /// <returns>Task of deleted contact response.</returns>
        Task<DeleteContactResponse> DeleteAsync(Guid id);
    }
}
