using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    public interface IContactHubClient
    {
        Task<ContactModel> GetAsync(Guid id);
    }
}
