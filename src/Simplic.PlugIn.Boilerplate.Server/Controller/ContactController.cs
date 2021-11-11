using Simplic.Framework.WebAPI.Core;
using Simplic.Boilerplate;

namespace Simplic.PlugIn.Boilerplate.Server
{
    [Framework.WebAPI.JWTAuthorize]
    public class ContactController : ExtendedApiController
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
    }
}
