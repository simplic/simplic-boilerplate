using Simplic.MessageBroker;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class ContactEventService : IContactEventService
    {
        private readonly IMessageBus messageBus;

        public ContactEventService(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public async Task SendCreatedEventAsync(Contact contact)
        {
            await Task.Run(() => messageBus.Publish<SchemaRegistry.ContactCreatedEvent>(GetEventValues(contact)));
        }

        public async Task SendDeletedEventAsync(Contact contact)
        {
            await Task.Run(() => messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(GetEventValues(contact)));
        }

        public async Task SendUpdatedEventAsync(Contact contact)
        {
            await Task.Run(() => messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(GetEventValues(contact)));
        }

        object GetEventValues(Contact contact)
        {
            return new
            {
                Contact = new
                {
                    contact.Id,
                    contact.IsDeleted,
                    contact.Name,
                    contact.Addresses
                }
            };
        }
    }
}
