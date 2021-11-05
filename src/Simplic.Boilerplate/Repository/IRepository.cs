﻿using System.Threading.Tasks;

namespace Simplic.Boilerplate
{
    public interface IRepository<TId, TObject>
    {
        Task<int> CommitAsync();
        Task CreateAsync(TObject document);
        Task DeleteAsync(TId id);
        Task UpdateAsync(TObject document);
    }
}
