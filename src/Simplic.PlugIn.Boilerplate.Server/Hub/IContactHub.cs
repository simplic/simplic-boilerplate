using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;
using Microsoft.AspNet.SignalR.Hubs;

namespace Simplic.PlugIn.Boilerplate.Server
{
    public interface IContactHub : IHub
    {
        Task<ContactModel> GetAsync(Guid id);
    }
}
