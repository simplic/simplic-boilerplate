using CommonServiceLocator;
using Simplic.Boilerplate.Client;
using Simplic.Boilerplate.Shared;
using Simplic.Collaboration.Client;
using Simplic.Collaboration.UI;
using Simplic.Configuration;
using Simplic.UI.MVC;
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

            AddAddressCommand = new RelayCommand((o) =>
            {
                Addresses.Add(new AddressViewModel
                {
                    Model = new AddressModel
                    {
                        Id = Guid.NewGuid(),
                    }, Parent = this
                });
            });

            RemoveAddressCommand = new RelayCommand((o) =>
            {
                if (SelectedAddress != null)
                {
                    Addresses.Remove(SelectedAddress);
                }
            });
        }

        public override async Task Initialize()
        {
            Client.Initialize();
            await base.Initialize();
        }

        public override async Task<DataSession> Edit(Guid id)
        {
            var session = await base.Edit(id);
            Addresses = new CollaborationObervableCollection<AddressViewModel, AddressModel>(Id, nameof(Addresses), "Id", Client);
            return session;
        }

        public override void OnClose()
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                HubClient.Leave(Model.Id, false, false);
            });

            base.OnClose();
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

        public RelayCommand AddAddressCommand { get; set; }
        public RelayCommand RemoveAddressCommand { get; set; }

        /// <summary>
        /// Gets or sets addresses.
        /// </summary>
        public CollaborationObervableCollection<AddressViewModel, AddressModel> Addresses { get; set; }
        public AddressViewModel SelectedAddress { get; set; }
    }
}
