using Simplic.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    /// <summary>
    /// Service for managing the contact object.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IContactEventService contactEventService;

        /// <summary>
        /// Initializes a new instance of the conatact service.
        /// </summary>
        /// <param name="contactEventService">Service for sending the events.</param>
        /// <param name="contactRepository">Repository for managing the contact.</param>
        public ContactService(IContactEventService contactEventService, IContactRepository contactRepository)
        {
            this.contactEventService = contactEventService;
            this.contactRepository = contactRepository;
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Contact contact)
        {
            await contactEventService.SendCreatedEventAsync(contact);
            await contactRepository.CreateAsync(contact);
            await contactRepository.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Contact contact)
        {
            await contactRepository.DeleteAsync(contact.Id);
            await contactRepository.CommitAsync();
            await contactEventService.SendDeletedEventAsync(contact.Id);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id)
        {
            await contactEventService.SendDeletedEventAsync(id);
            await contactRepository.DeleteAsync(id);
            await contactRepository.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Contact contact)
        {
            await contactEventService.SendUpdatedEventAsync(contact);
            await contactRepository.UpdateAsync(contact);
            await contactRepository.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task<Contact> GetAsync(Guid id)
        {
            return await contactRepository.GetAsync(id);
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Contact contact, ITransaction transaction)
        {
            await contactEventService.SendCreatedEventAsync(contact);
            await contactRepository.CreateAsync(contact, transaction);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Contact contact, ITransaction transaction)
        {
            await contactEventService.SendDeletedEventAsync(contact.Id);
            await contactRepository.DeleteAsync(contact.Id, transaction);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id, ITransaction transaction)
        {
            await contactEventService.SendDeletedEventAsync(id);
            await contactRepository.DeleteAsync(id, transaction);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Contact contact, ITransaction transaction)
        {
            await contactEventService.SendUpdatedEventAsync(contact);
            await contactRepository.UpdateAsync(contact, transaction);
        }

        /// <inheritdoc/>
        public async Task UpsertAsync(ContactFilter filter, Contact entity) 
            => await contactRepository.UpsertAsync(filter, entity);

        /// <inheritdoc/>
        public async Task<int> CommitAsync() 
            => await contactRepository.CommitAsync();

        /// <inheritdoc/>
        public async Task<IEnumerable<Contact>> GetAllAsync() 
            => await contactRepository.GetAllAsync();

        /// <inheritdoc/>
        public async Task<IEnumerable<Contact>> GetByFilterAsync(ContactFilter filter) 
            => await contactRepository.GetByFilterAsync(filter);

        /// <inheritdoc/>
        public async Task<IEnumerable<Contact>> FindAsync(ContactFilter predicate, int? skip, int? limit, string sortField = "", bool isAscending = true)
            => await contactRepository.FindAsync(predicate, skip, limit, sortField, isAscending);

        /// <inheritdoc/>
        public void Dispose() 
            => contactRepository.Dispose();
    }
}
