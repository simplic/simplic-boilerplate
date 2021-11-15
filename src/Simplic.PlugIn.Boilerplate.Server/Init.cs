using CommonServiceLocator;
using Microsoft.AspNet.SignalR;
using Simplic.Framework.Base;
using Simplic.Framework.PlugIn;
using System;
using Unity;
using Unity.Lifetime;
using AutoMapper;
using Simplic.Boilerplate;
using Simplic.Boilerplate.Service;
using Simplic.Boilerplate.Data.MongoDB;

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
            container.RegisterInstance("BoilerplateMapper", new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactMapperProfile());
            }).CreateMapper());

            // Register hub
            container.RegisterType<IContactHub, ContactHub>(new ContainerControlledLifetimeManager());
            container.RegisterFactory<IHubContext<IContactHubClient>>((_) =>
            {
                return GlobalHost.ConnectionManager.GetHubContext<ContactHub, IContactHubClient>();
            }, new ContainerControlledLifetimeManager());

            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IContactEventService, ContactEventService>();
            container.RegisterType<IContactRepository, ContactRepository>();
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
