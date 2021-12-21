using CommonServiceLocator;
using Simplic.Boilerplate.Client;
using Simplic.Boilerplate.Shared;
using Simplic.Configuration;
using Simplic.Framework.UI;
using Simplic.Studio.UI;
using Simplic.WebApi.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.UI
{
    /// <summary>
    /// View model of the contact.
    /// </summary>
    public class ContactViewModel : Collaboration.UI.CollaborationViewModel<ContactModel>
    {
        private readonly IClient client;
        private readonly IConnectionConfigurationService configurationService;

        public ContactViewModel()
        {

            client = ServiceLocator.Current.GetInstance<IClient>();
            configurationService = ServiceLocator.Current.GetInstance<IConnectionConfigurationService>();
            Client = new ContactHubClient(client, configurationService);

            RegisterProperty(x => x.Name, (x) => Model.Name = x);
        }

        public override async Task Initialize()
        {
            Client.Initialize();
            await base.Initialize();
        }

        internal async Task Edit(Guid id)
        {
            var session = await Client.Join(id);
            Model = await HubClient.Get(id);
        }

        protected override Guid GetId() => Id;

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Id { get => Model.Id; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set => PropertySetter(value, (v) => Model.Name = v, callOthers: true);
        }

        /// <summary>
        /// Gets or sets addresses.
        /// </summary>
        public ObservableCollection<AddressViewModel> Addresses { get; set; } = new ObservableCollection<AddressViewModel>();
    }
}
