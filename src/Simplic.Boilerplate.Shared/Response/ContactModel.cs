using System;

namespace Simplic.Boilerplate.Shared
{
    /// <summary>
    /// Response model for the contact.
    /// <para>Derives for <see cref="ContactBase"/>.</para>
    /// </summary>
    public class ContactModel : ContactBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
