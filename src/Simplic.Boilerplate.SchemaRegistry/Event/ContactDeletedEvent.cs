namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Event for contact deleted.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface ContactDeletedEvent
    {
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        Contact Contact { get; set; }
    }
}
