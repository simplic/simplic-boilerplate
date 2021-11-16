namespace Simplic.Boilerplate
{
    /// <summary>
    /// Repository for managing contact directly  and contact via transactions.
    /// </summary>
    public interface IContactRepository : IContactBaseRepository, IContactTransactionRepository { }
}
