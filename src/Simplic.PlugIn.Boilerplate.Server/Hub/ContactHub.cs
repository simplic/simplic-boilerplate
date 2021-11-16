using System;
using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Shared;
using System.Threading.Tasks;
using Unity;

namespace Simplic.PlugIn.Boilerplate.Server
{
    /// <inheritdoc cref="IContactHub"/>
    [Framework.WebAPI.SignalRJWTAuthorize]
    public class ContactHub : Hub<IContactHubClient>, IContactHub
    {
        private readonly IContactService contactService;
        private readonly AutoMapper.IMapper mapper;

        /// <summary>
        /// Initializes a new instace of contact hub.
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="mapper"></param>
        public ContactHub(IContactService contactService, [Dependency("BoilerplateMapper")] AutoMapper.IMapper mapper)
        {
            this.contactService = contactService;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
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
