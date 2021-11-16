namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Interface for the phone number.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface PhoneNumber
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        string Number { get; set; }
    }
}