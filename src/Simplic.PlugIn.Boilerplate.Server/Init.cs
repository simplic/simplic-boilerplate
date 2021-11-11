using CommonServiceLocator;
using Microsoft.AspNet.SignalR;
using Simplic.Framework.Base;
using Simplic.Framework.PlugIn;
using System;
using Unity;
using Unity.Lifetime;
using AutoMapper;

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

            // Register automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactMapperProfile());
            });

            var mapper = config.CreateMapper();
            container.RegisterInstance<IMapper>("BoilerplateMapper", mapper);

            // Register hub
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
    [PlugInDesc("Simplic.Server Contact", "1.0.0.0", "0a5e3ee8-18f5-49fa-9930-0d7b0e221eab")]
    public class Init
    {
        public static void Initialize()
        {

        }
    }
}
