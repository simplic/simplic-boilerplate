using Simplic.Data.MongoDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.Data.MongoDB
{
    public class ContactRepository : MongoRepositoryBase<Guid, Contact, ContactFilter>, IContactRepository
    {
        public ContactRepository(IMongoContext context) : base(context) { }

        public ContactRepository(IMongoContext context, string configurationKey) : base(context, configurationKey) { }
    }
}
