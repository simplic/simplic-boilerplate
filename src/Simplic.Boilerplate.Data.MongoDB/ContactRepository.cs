using Simplic.Data.MongoDB;
using System;

namespace Simplic.Boilerplate.Data.MongoDB
{
    /// <inheritdoc cref="IContactRepository"/>
    public class ContactRepository : MongoRepositoryBase<Guid, Contact, ContactFilter>, IContactRepository
    {
        /// <summary>
        /// Initialzes a new instance of contact repository.
        /// </summary>
        /// <param name="context">Mongo context.</param>
        public ContactRepository(IMongoContext context) : base(context) { }

        /// <summary>
        /// Initialzes a new instance of contact repository.
        /// </summary>
        /// <param name="context">Mongo context.</param>
        /// <param name="configurationKey">Configuration key for the database.</param>
        public ContactRepository(IMongoContext context, string configurationKey) : base(context, configurationKey) { }
    }
}
