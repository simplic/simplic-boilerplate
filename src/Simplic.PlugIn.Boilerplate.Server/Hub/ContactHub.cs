using System;
using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Shared;
using System.Threading.Tasks;
using Unity;

namespace Simplic.PlugIn.Boilerplate.Server
{
    [Framework.WebAPI.SignalRJWTAuthorize]
    public class ContactHub : Hub<IContactHubClient>, IContactHub
    {
        private readonly IContactService contactService;
        private readonly AutoMapper.IMapper mapper;

        public ContactHub(IContactService contactService, [Dependency("BoilerplateMapper")] AutoMapper.IMapper mapper)
        {
            this.contactService = contactService;
            this.mapper = mapper;
        }

        public async Task<ContactModel> GetAsync(Guid id)
        {
            try
            {
                var contact = await contactService.GetAsync(id);

                if (contact == null)
                    throw new Exception($"Coult not find resource by id {id}");

                // Return updated contact
                return mapper.Map<ContactModel>(contact);
            }
            catch (Exception ex)
            {
                // TODO: Add some logging (DI)

                throw;
            }
        }
    }
}
