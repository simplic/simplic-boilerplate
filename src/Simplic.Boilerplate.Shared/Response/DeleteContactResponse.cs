using System;

namespace Simplic.Boilerplate.Shared
{
    /// <summary>
    /// Delete response model for the contact.
    /// </summary>
    public class DeleteContactResponse
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets whether it was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
}
