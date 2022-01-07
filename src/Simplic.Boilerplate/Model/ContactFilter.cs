using Simplic.Data.NoSql;
using System;

namespace Simplic.Boilerplate
{
    public class ContactFilter : IFilter<Guid>
    {
        public Guid Id { get; set; }
    }
}
