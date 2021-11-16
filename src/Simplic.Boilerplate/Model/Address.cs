using System.Collections.Generic;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Class that holds the address data.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or setts the phone numbers.
        /// </summary>
        public IList<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    }
}