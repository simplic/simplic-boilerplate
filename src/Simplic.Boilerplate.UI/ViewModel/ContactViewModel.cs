using Simplic.Framework.UI;
using Simplic.Studio.UI;
using System;
using System.Collections.ObjectModel;

namespace Simplic.Boilerplate.UI
{
    /// <summary>
    /// View model of the contact.
    /// </summary>
    public class ContactViewModel : Collaboration.UI.CollaborationViewModel<Contact>, IWindowViewModel<Contact>
    {
        public void Initialize(Contact model)
        {
            Model = model;
        }

        protected override Guid GetId() => Guid;

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets addresses.
        /// </summary>
        public ObservableCollection<AddressViewModel> Addresses { get; set; } = new ObservableCollection<AddressViewModel>();

        public Contact Model { get; set; }
    }
}
