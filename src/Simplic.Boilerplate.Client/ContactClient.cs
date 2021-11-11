using System;
using System.Threading.Tasks;
using Simplic.WebApi.Client;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    public class ContactClient : WebApi.Client.ClientBase
    {
        public ContactClient(string url) : base(url)
        {
        }

        public ContactClient(IClient clientBase) : base(clientBase)
        {
        }

        public async Task<ContactModel> GetAsync(Guid id)
        {
            return await base.GetAsync<ContactModel>("", "Contact", $"/{id}", new System.Collections.Generic.Dictionary<string, string> { });
        }

        public async Task<ContactModel> CreateAsync(CreateContactRequest model)
        {
            return await base.PostAsync<ContactModel, CreateContactRequest>("", "Contact", $"create", model);
        }

        public async Task<ContactModel> UpdateAsync(UpdateContactRequest model)
        {
            return await base.PutAsync<ContactModel, UpdateContactRequest>("", "Contact", $"update", model);
        }

        public async Task<DeleteContactResponse> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync<DeleteContactResponse>("", "Contact", $"/{id}", new System.Collections.Generic.Dictionary<string, string> { });
        }
    }
}
