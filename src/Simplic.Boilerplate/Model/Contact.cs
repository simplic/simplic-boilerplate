using Simplic.Data.NoSql;
using System;
using System.Collections.Generic;

namespace Simplic.Boilerplate
{
    public class Contact : IDocument<Guid>
    {
        public string Name { get; set; }
        public IList<Address> Addresses { get; set; } = new List<Address>();
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; }
    }
}
