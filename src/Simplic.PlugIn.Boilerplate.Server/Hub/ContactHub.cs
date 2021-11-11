using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;

namespace Simplic.PlugIn.Boilerplate.Server
{
    [Framework.WebAPI.SignalRJWTAuthorize]
    public class ContactHub : Hub<IContactHubClient>, IContactHub
    {
        private readonly IContactService contactService;

        public ContactHub(IContactService contactService)
        {
            this.contactService = contactService;
        }
    }
}
