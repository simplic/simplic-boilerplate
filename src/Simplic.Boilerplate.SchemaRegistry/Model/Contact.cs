using System;
using System.Collections.Generic;

namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Interface for the contact.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface Contact
    {
        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        IList<Address> Addresses { get; set; }
    }
}
