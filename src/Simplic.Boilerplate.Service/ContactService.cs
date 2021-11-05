using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public Task<int> CommitAsync() => contactRepository.CommitAsync();

        public Task CreateAsync(Contact document) => contactRepository.CreateAsync(document);

        public Task DeleteAsync(Guid id) => contactRepository.DeleteAsync(id);

        public Task UpdateAsync(Contact document) => contactRepository.UpdateAsync(document);
    }
}
