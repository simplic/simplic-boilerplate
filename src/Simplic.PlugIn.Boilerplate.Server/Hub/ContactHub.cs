using Microsoft.AspNet.SignalR;

namespace Simplic.PlugIn.Boilerplate.Server
{
    [Framework.WebAPI.SignalRJWTAuthorize]
    public class ContactHub : Hub<IContactHubClient>, IContactHub
    {
        
    }
}
