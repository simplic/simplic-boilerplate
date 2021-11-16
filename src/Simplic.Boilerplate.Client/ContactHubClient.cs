using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    /// <inheritdoc cref="IContactHubClient"/>
    public class ContactHubClient : WebApi.Client.HubClientBase, IContactHubClient
    {
        /// <summary>
        /// Initializes a new instance of the contact hub client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="connectionConfigurationService">Connection configuration service.</param>
        protected ContactHubClient(WebApi.Client.IClient client, Configuration.IConnectionConfigurationService connectionConfigurationService) : base(client, connectionConfigurationService)
        {
        }

        /// <summary>
        /// Initializes the contact hub client.
        /// </summary>
        /// <param name="connectionName">Connection name.</param>
        public override void Initialize(string connectionName = "SimplicWebApi")
        {
            base.Initialize(connectionName);
        }

        /// <inheritdoc/>
        public async Task<ContactModel> GetAsync(Guid id)
        {
            return await Proxy.Invoke<ContactModel>(nameof(GetAsync), id);
        }

        /// <summary>
        /// Gets the hub name ContactHub
        /// </summary>
        public override string HubName => "ContactHub";
    }
}
