using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    public interface IContactClient
    {
        Task<ContactModel> GetAsync(Guid id);

        Task<ContactModel> CreateAsync(CreateContactRequest model);

        Task<ContactModel> UpdateAsync(UpdateContactRequest model);

        Task<DeleteContactResponse> DeleteAsync(Guid id);
    }
}
