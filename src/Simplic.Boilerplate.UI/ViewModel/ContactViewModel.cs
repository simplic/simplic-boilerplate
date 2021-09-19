using System;
using System.Collections.ObjectModel;

namespace Simplic.Boilerplate.UI
{
    public class ContactViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public ObservableCollection<AddressViewModel> Addresses { get; set; } = new ObservableCollection<AddressViewModel>();
    }
}
