using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.SchemaRegistry
{
    public interface Contact
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        IList<Address> Addresses { get; set; }
    }
}
