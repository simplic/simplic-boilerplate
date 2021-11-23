using System;
using Microsoft.AspNet.SignalR;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Shared;
using System.Threading.Tasks;
using Unity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<IList<DataSession>> JoinMultipleAsync(IList<Guid> ids)
        {
            var sessions = new List<DataSession>();

            foreach (var id in ids)
            {
                sessions.Add(await JoinAsync(id));
            }

            return sessions;
        }

        public async Task<DataSession> JoinAsync(Guid id)
        {
            var session = new DataSession();

            await Groups.Add(Context.ConnectionId, $"Session_{id}");

            return session;
        }

        public async Task LeaveMultipleAsync(IList<(Guid, bool)> sessions)
        {
            foreach (var session in sessions)
            {
                await LeaveAsync(session.Item1, session.Item2);
            }
        }

        public async Task LeaveAsync(Guid id, bool commit)
        {
            if (commit)
            {
                await CommitChangesAsync(id);
            }
            else
            {
                await AbortChangesAsync(id);
            }

            await Groups.Remove(Context.ConnectionId, $"Session_{id}");
        }

        public async Task ChangeFieldAsync(Guid sessionId, string path, JProperty property)
        {
            //Session currentSession = null;

            //currentSession.Object
        }

        public async Task AddToCollectionAsync()
        {

        }

        public async Task RemoveFromCollectionAsync()
        {

        }

        public async Task CommitChangesAsync(Guid id)
        {
            // Save changes to the database
            // Send information to other participants
        }

        public async Task AbortChangesAsync(Guid id)
        {
            // Send information about abortion to all participants
        }
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
        public Leaf(JValue other) : base(other)
        {
        }

        public Leaf(long value) : base(value)
        {
        }

        public Leaf(decimal value) : base(value)
        {
        }

        public Leaf(char value) : base(value)
        {
        }

        public Leaf(ulong value) : base(value)
        {
        }

        public Leaf(double value) : base(value)
        {
        }

        public Leaf(float value) : base(value)
        {
        }

        public Leaf(DateTime value) : base(value)
        {
        }

        public Leaf(DateTimeOffset value) : base(value)
        {
        }

        public Leaf(bool value) : base(value)
        {
        }

        public Leaf(string value) : base(value)
        {
        }

        public Leaf(Guid value) : base(value)
        {
        }

        public Leaf(Uri value) : base(value)
        {
        }

        public Leaf(TimeSpan value) : base(value)
        {
        }

        public Leaf(object value) : base(value)
        {
        }
    }
}
