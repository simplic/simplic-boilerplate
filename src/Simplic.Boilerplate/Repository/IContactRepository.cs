using System;

namespace Simplic.Boilerplate
{
    public interface IContactRepository : IMongoDbRepository<Guid, Contact>
    {
    }
}
