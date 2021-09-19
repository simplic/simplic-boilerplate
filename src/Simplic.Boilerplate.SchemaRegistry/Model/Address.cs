using System.Collections.Generic;

namespace Simplic.Boilerplate.SchemaRegistry
{
    public interface Address
    {
        string Street { get; set; }
        string HouseNumber { get; set; }
        string City { get; set; }
        string Zipcode { get; set; }
        string Country { get; set; }
        IList<PhoneNumber> PhoneNumbers { get; set; }
    }
}