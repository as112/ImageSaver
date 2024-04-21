using System;
using System.Collections.Generic;
using System.Threading;
using TestSystem.Core.Models;

namespace TestSystem.Client.Services.Interfaces
{
    public interface IDataService<T> where T : BaseFile
    {
        Task<T?> SendDataAsync(T item, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
