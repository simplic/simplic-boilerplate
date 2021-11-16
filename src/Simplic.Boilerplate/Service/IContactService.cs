namespace Simplic.Boilerplate
{
    /// <summary>
    /// Service for managing contact directly  and contact via transactions.
    /// </summary>
    public interface IContactService : IContactTransactionService, IContactBaseService { }
}
