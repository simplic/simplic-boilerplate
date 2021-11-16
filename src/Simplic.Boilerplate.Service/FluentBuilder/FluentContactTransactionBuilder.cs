using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class FluentContactTransactionBuilder
    {
        private readonly IContactRepository contactRepository;
        private readonly IList<Action> actions = new List<Action>();

        public FluentContactTransactionBuilder(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public ICanAddOrCommitTransaction AddCreate(Contact contact)
        {
            if (contact != null)
                actions.Add(async () => await contactRepository.CreateAsync(contact));
            return this;
        }

        public ICanAddOrCommitTransaction AddDelete(Contact contact)
        {
            if (contact != null)
                actions.Add(async () => await contactRepository.DeleteAsync(contact.Id));
            return this;
        }

        public ICanAddOrCommitTransaction AddUpdate(Contact contact)
        {
            if (contact != null)
                actions.Add(async () => await contactRepository.UpdateAsync(contact));
            return this;
        }

        public async Task<int> CommitAsync()
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }

            return await contactRepository.CommitAsync();
        }
    }
}
