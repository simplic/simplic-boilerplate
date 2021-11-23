using Simplic.Data.NoSql;
using System;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Contact filter for mongo db.
    /// <para>Derives from <see cref="IFilter{TId}"/>.</para>
    /// </summary>
    public class ContactFilter : IFilter<Guid>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
