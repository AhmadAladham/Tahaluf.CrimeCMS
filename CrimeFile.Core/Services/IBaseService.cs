using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IBaseService
    {
        ServiceResult<T> Execute<T>(Func<IDBContext, ServiceResult<T>> func);

        ServiceResult Execute(Func<IDBContext, ServiceResult> func);

        Task<ServiceResult> ExecuteAsync(Func<IDBContext, Task<ServiceResult>> func);

        Task<ServiceResult<T>> ExecuteAsync<T>(Func<IDBContext, Task<ServiceResult<T>>> func);
    }
}
