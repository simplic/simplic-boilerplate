using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Simplic.Boilerplate.UI
{
    public class AddressViewModel
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public ObservableCollection<PhoneNumberViewModel> PhoneNumbers { get; set; } = new ObservableCollection<PhoneNumberViewModel>();
    }
}