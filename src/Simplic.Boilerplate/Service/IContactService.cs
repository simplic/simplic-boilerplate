using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IContactService
    {
        Task<int> CreateAsync(Contact contact);
        Task<int> DeleteAsync(Contact contact);
        Task<int> UpdateAsync(Contact contact);
    }
}
