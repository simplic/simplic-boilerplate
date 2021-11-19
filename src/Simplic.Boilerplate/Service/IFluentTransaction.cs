using Simplic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IFluentTransaction
    {
        IFluentTransaction AddCreate(Contact contact);
        IFluentTransaction AddUpdate(Contact contact);
        IFluentTransaction AddDelete(Contact contact);
        Task CommitAsync();
        Task AbortAsync();
    }

    class MyClass
    {
        public async Task Test()
        {
            IContactService service = null;
            IFluentTransactionBuilder builder = null;

            await builder.BeginTransaction()
                         .AddService<IContactService, Contact, Guid>(service)
                            .Create<IContactService, Contact, Guid>(x => new Contact())
                            .Update<IContactService, Contact, Guid>(x => new Contact())
                         .CommitAsync();
        }
    }

    public interface IFluentTransactionBuilder
    {
        IFluentTransactionBuilder BeginTransaction();
    }

    public static class FluentTransaction
    {
        public static IFluentTransactionBuilder AddService<K, T, I>(this IFluentTransactionBuilder builder, K service) where K : ITransactionRepository<T, I>
                                                                                                                       where T : new()
        {
            return builder;
        }

        public static IFluentTransactionBuilder Create<K, T, I>(this IFluentTransactionBuilder builder, Func<K, T> obj) where K: ITransactionRepository<T, I>
                                                                                                                     where T : new()
        {
            return null;
        }

        public static IFluentTransactionBuilder Update<K, T, I>(this IFluentTransactionBuilder builder, Func<K, T> obj) where K : ITransactionRepository<T, I>
                                                                                                                     where T : new()
        {
            return null;
        }

        public static IFluentTransactionBuilder Delete<K, T, I>(this IFluentTransactionBuilder builder, Func<K, T> obj) where K : ITransactionRepository<T, I>
                                                                                                                     where T : new()
        {
            return null;
        }

        public static async Task CommitAsync(this IFluentTransactionBuilder builder)
        {

        }

        public static async Task AbortAsync(this IFluentTransactionBuilder builder)
        {

        }
    }

}
