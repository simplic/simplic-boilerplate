using System;
using System.Collections.Generic;

namespace Simplic.Boilerplate.SchemaRegistry
{
    /// <summary>
    /// Interface for the address.
    /// <para>Used for the rabbitmq message broker.</para>
    /// </summary>
    public interface Address
    {
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        string Zipcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        IList<PhoneNumber> PhoneNumbers { get; set; }
        Guid Id { get; }
    }
}