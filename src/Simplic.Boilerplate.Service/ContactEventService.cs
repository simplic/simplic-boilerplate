using Simplic.MessageBroker;
using System;
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
            messageBus.Publish<SchemaRegistry.ContactCreatedEvent>(GetEventValues(contact));
        }

        public async Task SendDeletedEventAsync(Contact contact)
        {
            messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(GetEventValues(contact));
        }

        public async Task SendDeletedEventAsync(Guid contactId)
        {
            messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(new { ContactId = contactId });
        }

        public async Task SendUpdatedEventAsync(Contact contact)
        {
            messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(GetEventValues(contact));
        }

        object GetEventValues(Contact contact)
        {
            return new
            {
                Contact = contact
            };
        }
    }
}
