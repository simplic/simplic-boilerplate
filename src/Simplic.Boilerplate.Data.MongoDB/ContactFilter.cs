using Simplic.Data.NoSql;
using System;

namespace Simplic.Boilerplate.Data.MongoDB
{
    public class ContactFilter : IFilter<Guid>
    {
        public Guid Id { get; set; }
    }
}
