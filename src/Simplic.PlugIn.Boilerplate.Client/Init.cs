using Unity;
using Simplic.Boilerplate.Client;

namespace Simplic.PlugIn.Boilerplate.Client
{
    /// <summary>
    /// Root PlugIn class
    /// </summary>
    [Framework.PlugIn.PlugInDesc("Simplic.Client Contact", "1.0.0.0", "488b1e6f-967e-4342-a829-695299a98384")]
    public class Init
    {
        public static void Initialize()
        {
            var container = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUnityContainer>();

            container.RegisterType<IContactClient, ContactClient>();
            container.RegisterType<IContactHubClient, ContactHubClient>();
        }
    }
}
