namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Event for contact updated.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface ContactUpdatedEvent
    {
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        Contact Contact { get; set; }
    }
}
