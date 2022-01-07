using System;
using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Shared;
using System.Threading.Tasks;
using Unity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Simplic.Collaboration;

namespace Simplic.PlugIn.Boilerplate.Server
{
    /// <inheritdoc cref="IContactHub"/>
    [Framework.WebAPI.SignalRJWTAuthorize]
    public class ContactHub : Collaboration.Hub.CollaborationHub<IContactHubClient, Contact>, IContactHub
    {
        private readonly IContactService contactService;
        private readonly AutoMapper.IMapper mapper;

        /// <summary>
        /// Initializes a new instace of contact hub.
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="mapper"></param>
        public ContactHub(IContactService contactService, [Dependency("BoilerplateMapper")] AutoMapper.IMapper mapper, IDataSessionService dataSessionService) : base (dataSessionService)
        {
            this.contactService = contactService;
            this.mapper = mapper;
        }

        protected override async Task<Contact> GetData(Guid id)
        {
            return new Contact
            {
                Id = id,
                Name= "Hallo",
            };
            //return await contactService.GetAsync(id);
        }

        protected override async Task SaveData(Contact data)
        {
            await contactService.UpdateAsync(data);
        }
    }
}
