using System;
using System.Collections.Generic;
using System.Text;

namespace Simplic.Boilerplate.Shared
{
    public class UpdateContactRequest : ContactBase
    {
        public Guid Guid { get; set; }
    }
}
