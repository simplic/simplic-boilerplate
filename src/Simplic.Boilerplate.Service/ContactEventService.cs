using Simplic.MessageBroker;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    /// <inheritdoc/>
    public class ContactEventService : IContactEventService
    {
        private readonly IMessageBus messageBus;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="messageBus"><see cref="IMessageBus"/>.</param>
        public ContactEventService(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        /// <inheritdoc/>
        public async Task SendCreatedEventAsync(Contact contact)
        {
            messageBus.Publish<SchemaRegistry.ContactCreatedEvent>(GetEventValues(contact));
        }

        /// <inheritdoc/>
        public async Task SendDeletedEventAsync(Contact contact)
        {
            messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(GetEventValues(contact));
        }

        /// <inheritdoc/>
        public async Task SendDeletedEventAsync(Guid contactId)
        {
            messageBus.Publish<SchemaRegistry.ContactDeletedEvent>(new { ContactId = contactId });
        }

        /// <inheritdoc/>
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
