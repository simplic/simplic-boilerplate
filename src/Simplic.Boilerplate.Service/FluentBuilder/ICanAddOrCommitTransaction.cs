using System.Threading.Tasks;

namespace Simplic.Boilerplate.Service
{
    public interface ICanAddOrCommitTransaction
    {
        ICanAddOrCommitTransaction AddCreate(Contact contact);
        ICanAddOrCommitTransaction AddUpdate(Contact contact);
        ICanAddOrCommitTransaction AddDelete(Contact contact);
        Task<int> CommitAsync();
    }
}
