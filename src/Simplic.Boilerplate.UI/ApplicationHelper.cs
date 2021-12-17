using CommonServiceLocator;
using Simplic.Boilerplate.Client;
using Simplic.Configuration;
using Simplic.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.UI
{
    public static class ApplicationHelper
    {
        public static void OpenSignalRWindow(Guid appId)
        {
            var client = ServiceLocator.Current.GetInstance<IClient>();
            var configurationService = ServiceLocator.Current.GetInstance<IConnectionConfigurationService>();

            var hubClient = new ContactHubClient(client, configurationService);

            var hubClient2 = new ContactHubClient(client, configurationService);

            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                var id = Guid.NewGuid();

                hubClient.Initialize();
                var session = await hubClient.Join(id);

                hubClient.UserJoins += (s, e) => 
                {
                
                };

                hubClient2.Initialize();
                var session2 = await hubClient2.Join(id);
                hubClient2.FieldChanged += (s, e) => 
                {
                
                };

                hubClient2.UserLeaves += (s, e) => 
                {
                
                };

                var t = await hubClient.Get(session.DataId);
                await hubClient.ChangeFieldString(session.DataId, "$.Name", "Nicht mehr Hallo");

                var t2 = await hubClient.Get(session.DataId);

                await hubClient.Leave(id, false, false);
            });
        }
    }
}
