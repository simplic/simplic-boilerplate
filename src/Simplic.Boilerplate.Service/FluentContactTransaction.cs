using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class FluentContactTransaction : IFluentTransaction
    {
        private readonly IContactService contactService;
        private readonly Data.ITransaction transaction;

        public FluentContactTransaction(IContactService contactService, Data.ITransaction transaction)
        {
            this.contactService = contactService;
            this.transaction = transaction;
        }

        public IFluentTransaction AddCreate(Contact contact)
        {
            contactService.CreateAsync(contact, transaction);
            return this;
        }

        public IFluentTransaction AddDelete(Contact contact)
        {
            contactService.DeleteAsync(contact.Id, transaction);
            return this;
        }

        public IFluentTransaction AddUpdate(Contact contact)
        {
            contactService.UpdateAsync(contact, transaction);
            return this;
        }

        public IFluentTransaction CreateAsync()
        {
            return (IFluentTransaction)contactService.CreateTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await contactService.CommitAsync(transaction);
        }

        public async Task AbortAsync()
        {
            await contactService.AbortAsync(transaction);
        }
    }
}
