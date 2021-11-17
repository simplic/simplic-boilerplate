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
}
