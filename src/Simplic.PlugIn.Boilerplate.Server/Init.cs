using CommonServiceLocator;
using Microsoft.AspNet.SignalR;
using Simplic.Framework.Base;
using Simplic.Framework.PlugIn;
using System;
using Unity;
using Unity.Lifetime;

namespace Simplic.PlugIn.Boilerplate.Server
{
    public class InitFramework : IFrameworkEntryPoint
    {
        public Type[] DependingEntryPoints()
        {
            return null;
        }

        public void Initilize()
        {
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

            Console.WriteLine("Initialize task services");

            container.RegisterType<IContactHub, ContactHub>(new ContainerControlledLifetimeManager());
            container.RegisterFactory<IHubContext<IContactHubClient>>((_) =>
            {
                return GlobalHost.ConnectionManager.GetHubContext<ContactHub, IContactHubClient>();
            }, new ContainerControlledLifetimeManager());
        }
    }

    /// <summary>
    /// Root PlugIn class
    /// </summary>
    [PlugInDesc("Simplic Task", "1.0.221.1108", "9b72f404-799b-4d5a-9cb9-12fda65c42a7")]
    public class Init
    {
        public static void Initialize()
        {

        }
    }
}
