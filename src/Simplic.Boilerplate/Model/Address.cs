using System.Collections.Generic;

namespace Simplic.Boilerplate
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
    }
}