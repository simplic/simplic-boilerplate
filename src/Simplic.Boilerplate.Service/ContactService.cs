using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IContactEventService contactEventService;

        public ContactService(IContactEventService contactEventService, IContactRepository contactRepository)
        {
            this.contactEventService = contactEventService;
            this.contactRepository = contactRepository;
        }


        public async Task CreateAsync(Contact contact)
        {
            await contactEventService.SendCreatedEventAsync(contact);
            await contactRepository.CreateAsync(contact);
            await contactRepository.CommitAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            await contactEventService.SendDeletedEventAsync(contact);
            await contactRepository.DeleteAsync(contact.Id);
            await contactRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = await GetAsync(id);

            await contactEventService.SendDeletedEventAsync(contact);
            await contactRepository.DeleteAsync(id);
            await contactRepository.CommitAsync();
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            return await contactRepository.GetAsync(id);
        }

        public async Task UpdateAsync(Contact contact)
        {
            await contactEventService.SendUpdatedEventAsync(contact);
            await  contactRepository.UpdateAsync(contact);
            await contactRepository.CommitAsync();
        }
    }
}
