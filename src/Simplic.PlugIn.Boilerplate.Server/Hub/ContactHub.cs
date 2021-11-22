using System;
using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Shared;
using System.Threading.Tasks;
using Unity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Simplic.PlugIn.Boilerplate.Server
{
    public class CustomProperty : JProperty
    {
        public CustomProperty(JProperty other) : base(other)
        {
        }

        public CustomProperty(string name, params object[] content) : base(name, content)
        {
        }

        public CustomProperty(string name, object content) : base(name, content)
        {
        }

        public string Owner { get; set; }
        public int VersionId { get; set; }
    }

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

        public async Task<ContactModel> CreateAsync(CreateContactRequest model)
        {
            try
            {
                var contact = mapper.Map<Contact>(model);

                await contactService.CreateAsync(contact);

                return mapper.Map<ContactModel>(contact);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DataSession> JoinAsync(IList<Guid> ids)
        {
            var session = new DataSession();

            await Groups.Add(Context.ConnectionId, $"Session_{id}");

            return session;
        }

        public async Task<DataSession> LeaveAsync(Guid id, bool commit)
        {
            var session = new DataSession();

            if (commit)
            {
                // Save changes to the database
                // Send information to other participants
            }
            else
            {
                // Send information about abortion to all participants
            }

            await Groups.Remove(Context.ConnectionId, $"Session_{id}");

            return session;
        }

        public class SessionDataTree
        { 
            public Guid Id { get; set; }
        
            public JObject Trunk { get; set; }
        }

        public class Branch : JProperty // 
        {
            public Branch(JProperty other) : base(other)
            {
            }

            public Branch(string name, params object[] content) : base(name, content)
            {
            }

            public Branch(string name, object content) : base(name, content)
            {
            }
        }

        public class Leaf : JValue // CHangeset
        {
            public Leaf(JProperty other) : base(other)
            {
            }

            public Leaf(string name, params object[] content) : base(name, content)
            {
            }

            public Leaf(string name, object content) : base(name, content)
            {
            }
        }

        public async Task ChangeFieldAsync(Guid sessionId, string path, JProperty property)
        {
            Session currentSession = null;

             currentSession.Object
        }

        public async Task AddToCollectionAsync()
        {

        }

        public async Task RemoveFromCollectionAsync()
        {

        }

        public async Task CommitChangesAsync()
        {

        }
    }
}
