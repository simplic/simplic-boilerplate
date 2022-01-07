using Simplic.Data.NoSql;
using System;
using System.Collections.Generic;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Class that holds contact data.
    /// <para>Derives from <see cref="IDocument{TId}"/>.</para>
    /// </summary>
    public class Contact : IDocument<Guid>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        public IList<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the is deleted flag.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
