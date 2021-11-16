using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    /// <summary>
    /// Client for the contact hub.
    /// </summary>
    public interface IContactHubClient
    {
        /// <summary>
        /// Retrieves a contact model.
        /// </summary>
        /// <param name="id">Identifier of the contact model.</param>
        /// <returns>Task of contact model.</returns>
        Task<ContactModel> GetAsync(Guid id);
    }
}
