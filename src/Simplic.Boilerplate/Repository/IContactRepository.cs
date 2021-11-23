using Simplic.Data;
using Simplic.Data.NoSql;
using System;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Repository for managing contact directly  and contact via transactions.
    /// </summary>
    public interface IContactRepository : ITransactionRepository<Contact, Guid>, IRepository<Guid, Contact, ContactFilter>
    {
    }
}
