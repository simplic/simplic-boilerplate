using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IContactEventService contactEventService;
        private readonly FluentContactTransactionBuilder fluentContactTransactionBuilder;

        public ContactService(IContactEventService contactEventService, IContactRepository contactRepository)
        {
            this.contactEventService = contactEventService;
            this.contactRepository = contactRepository;
            fluentContactTransactionBuilder = new FluentContactTransactionBuilder(contactRepository);
        }

        public async Task<int> CreateAsync(Contact contact)
        {
            await contactEventService.SendCreatedEventAsync(contact);

            return await fluentContactTransactionBuilder.AddCreate(contact).CommitAsync();
        }

        public async Task<int> DeleteAsync(Contact contact)
        {
            await contactEventService.SendDeletedEventAsync(contact);
            await contactRepository.DeleteAsync(contact.Id);
            return await contactRepository.CommitAsync();
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            await contactEventService.SendUpdatedEventAsync(contact);
            await  contactRepository.UpdateAsync(contact);
            return await contactRepository.CommitAsync();
        }
    }
}
