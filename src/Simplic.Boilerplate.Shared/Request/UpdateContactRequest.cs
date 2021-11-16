using System;

namespace Simplic.Boilerplate.Shared
{
    /// <summary>
    /// Request model for updating a contact.
    /// <para>Derives for <see cref="ContactBase"/>.</para>
    /// </summary>
    public class UpdateContactRequest : ContactBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
