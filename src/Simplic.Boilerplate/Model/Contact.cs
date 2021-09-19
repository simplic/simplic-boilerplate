using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public class Contact
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public IList<Address> Addresses { get; set; } = new List<Address>();
    }
}
