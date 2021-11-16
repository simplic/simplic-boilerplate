using System;
using System.Threading.Tasks;
using Simplic.WebApi.Client;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    /// <inheritdoc cref="IContactClient"/>
    public class ContactClient : ClientBase, IContactClient
    {
        /// <summary>
        /// Initializes a new instance of the contact client.
        /// </summary>
        /// <param name="url">Url.</param>
        public ContactClient(string url) : base(url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the contact client.
        /// </summary>
        /// <param name="clientBase">Clientbase.</param>
        public ContactClient(IClient clientBase) : base(clientBase)
        {
        }

        /// <inheritdoc/>
        public async Task<ContactModel> GetAsync(Guid id)
        {
            return await base.GetAsync<ContactModel>("", "Contact", $"/{id}", new System.Collections.Generic.Dictionary<string, string> { });
        }

        /// <inheritdoc/>
        public async Task<ContactModel> CreateAsync(CreateContactRequest model)
        {
            return await base.PostAsync<ContactModel, CreateContactRequest>("", "Contact", $"create", model);
        }

        /// <inheritdoc/>
        public async Task<ContactModel> UpdateAsync(UpdateContactRequest model)
        {
            return await base.PutAsync<ContactModel, UpdateContactRequest>("", "Contact", $"update", model);
        }

        /// <inheritdoc/>
        public async Task<DeleteContactResponse> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync<DeleteContactResponse>("", "Contact", $"/{id}", new System.Collections.Generic.Dictionary<string, string> { });
        }
    }
}
