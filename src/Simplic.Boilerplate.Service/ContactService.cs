using Simplic.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IContactEventService contactEventService;
        private readonly ITransactionService transactionService;

        private IList<Action> transactionEventActions = new List<Action>();

        public ContactService(IContactEventService contactEventService, IContactRepository contactRepository, ITransactionService transactionService)
        {
            this.contactEventService = contactEventService;
            this.contactRepository = contactRepository;
            this.transactionService = transactionService;
        }

        public async Task CreateAsync(Contact contact)
        {
            await contactEventService.SendCreatedEventAsync(contact);
            await contactRepository.CreateAsync(contact);
            await contactRepository.CommitAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            await contactEventService.SendDeletedEventAsync(contact.Id);
            await contactRepository.DeleteAsync(contact.Id);
            await contactRepository.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await contactEventService.SendDeletedEventAsync(id);
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
            await contactRepository.UpdateAsync(contact);
            await contactRepository.CommitAsync();
        }


        public async Task<ITransaction> CreateTransactionAsync()
        {
            return await transactionService.CreateAsync();
        }

        public async Task AbortAsync(ITransaction transaction)
        {
            await transactionService.AbortAsync(transaction);
            transactionEventActions.Clear();
        }

        public async Task CommitAsync(ITransaction transaction)
        {
            await transactionService.CommitAsync(transaction);
            foreach (var action in transactionEventActions)
            {
                action.Invoke();
            }
            transactionEventActions.Clear();
        }

        public async Task CreateAsync(Contact contact, ITransaction transaction)
        {
            transactionEventActions.Add(async () => await contactEventService.SendCreatedEventAsync(contact));
            await contactRepository.CreateAsync(contact, transaction);
        }

        public async Task DeleteAsync(Contact contact, ITransaction transaction)
        {
            transactionEventActions.Add(async () => await contactEventService.SendDeletedEventAsync(contact.Id));
            await contactRepository.DeleteAsync(contact.Id, transaction);
        }

        public async Task DeleteAsync(Guid id, ITransaction transaction)
        {
            transactionEventActions.Add(async () => await contactEventService.SendDeletedEventAsync(id));
            await contactRepository.DeleteAsync(id, transaction);
        }

        public async Task UpdateAsync(Contact contact, ITransaction transaction)
        {
            transactionEventActions.Add(async () => await contactEventService.SendUpdatedEventAsync(contact));
            await contactRepository.UpdateAsync(contact, transaction);
        }
    }
}
