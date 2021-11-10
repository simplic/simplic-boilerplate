using Simplic.Data.MongoDB;
using System;

namespace Simplic.Boilerplate.Data.MongoDB
{
    public class ContactRepository : MongoRepositoryBase<Guid, Contact, ContactFilter>
    {
        public ContactRepository(IMongoContext context) : base(context) { }

        public ContactRepository(IMongoContext context, string configurationKey) : base(context, configurationKey) { }
    }
}
