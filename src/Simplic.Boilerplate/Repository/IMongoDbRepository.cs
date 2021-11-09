using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IMongoDbRepository<TId, TObject>
    {
        Task<int> CommitAsync();
        Task CreateAsync(TObject document);
        Task DeleteAsync(TId id);
        Task UpdateAsync(TObject document);
    }
}
