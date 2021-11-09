using System;
using System.Collections.Generic;

namespace Simplic.Boilerplate.SchemaRegistry
{
    public interface Contact
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        IList<Address> Addresses { get; set; }
    }
}
