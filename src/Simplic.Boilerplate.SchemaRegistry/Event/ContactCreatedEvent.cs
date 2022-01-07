namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Event for contact created.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface ContactCreatedEvent
    {
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        Contact Contact { get; set; }
    }
}
