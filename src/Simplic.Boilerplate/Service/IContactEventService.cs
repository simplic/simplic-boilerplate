using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IContactEventService
    {
        Task SendCreatedEventAsync(Contact contact);
        Task SendUpdatedEventAsync(Contact contact);
        Task SendDeletedEventAsync(Guid contactId);
    }
}
