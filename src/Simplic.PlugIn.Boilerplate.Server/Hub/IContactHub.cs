using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;
using Microsoft.AspNet.SignalR.Hubs;

namespace Simplic.PlugIn.Boilerplate.Server
{
    /// <summary>
    /// Interface for the contact hub.
    /// </summary>
    public interface IContactHub : IHub
    {
        /// <summary>
        /// Asynchronously retrieves a contact.
        /// </summary>
        /// <param name="id">Identifer of the contact.</param>
        /// <returns>Task of contact model.</returns>
        Task<ContactModel> GetAsync(Guid id);
    }
}
