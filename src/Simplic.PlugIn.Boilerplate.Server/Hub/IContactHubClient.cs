using System.Threading.Tasks;

namespace Simplic.PlugIn.Boilerplate.Server
{
    /// <summary>
    /// Interface for the contact hub client.
    /// </summary>
    public interface IContactHubClient
    {
        Task OnReceiveChanges();
    }
}
