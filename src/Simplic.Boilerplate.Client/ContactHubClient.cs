using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    /// <inheritdoc cref="IContactHubClient"/>
    public class ContactHubClient : Collaboration.Client.CollaborationClient<ContactModel>, IContactHubClient
    {
        /// <summary>
        /// Initializes a new instance of the contact hub client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="connectionConfigurationService">Connection configuration service.</param>
        public ContactHubClient(WebApi.Client.IClient client
            , Configuration.IConnectionConfigurationService connectionConfigurationService)
            : base(client, connectionConfigurationService)
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

        /// <summary>
        /// Gets the hub name ContactHub
        /// </summary>
        public override string HubName => "ContactHub";
    }
}
