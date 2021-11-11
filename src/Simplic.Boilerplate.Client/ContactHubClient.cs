using System;
using System.Threading.Tasks;
using Simplic.Boilerplate.Shared;

namespace Simplic.Boilerplate.Client
{
    public class ContactHubClient : WebApi.Client.HubClientBase
    {
        protected ContactHubClient(WebApi.Client.IClient client, Configuration.IConnectionConfigurationService connectionConfigurationService) : base(client, connectionConfigurationService)
        {

        }

        public override void Initialize(string connectionName = "SimplicWebApi")
        {
            base.Initialize(connectionName);
        }

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
