using Simplic.Boilerplate.Shared;
using System;
using System.Collections.ObjectModel;

namespace Simplic.Boilerplate.UI
{
    /// <summary>
    /// View model of the address.
    /// </summary>
    public class AddressViewModel : Collaboration.UI.CollaborationItemViewModelBase<AddressModel>
    {
        public Guid Id => Model.Id;

        public AddressViewModel()
        {
            RegisterProperty(x => x.Street, (x) => Model.Street = x);
        }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string Street
        {
            get => Model.Street;
            set => PropertySetter(value, (v) => Model.Street = v, callOthers: true);
        }

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
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        public ObservableCollection<PhoneNumberViewModel> PhoneNumbers { get; set; } = new ObservableCollection<PhoneNumberViewModel>();
    }
}