using CrimeFile.Core.Common;
using CrimeFile.Core.Services;
using CrimeFile.Infra.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Services
{
    public class BaseService : IBaseService, IDisposable
    {
        protected IUnitOfWork UnitOfWork { get; }

        protected IConfiguration Configuration { get; }
        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }

        public ServiceResult<T> Execute<T>(Func<IDBContext, ServiceResult<T>> func)
        {
            return With.Transaction(UnitOfWork, context =>
            {
                var result = func(context);
                return result;
            });
        }

        public ServiceResult Execute(Func<IDBContext, ServiceResult> func)
        {
            return With.Transaction(UnitOfWork, context =>
            {
                return func(context);
            });
        }

        public Task<ServiceResult> ExecuteAsync(Func<IDBContext, Task<ServiceResult>> func)
        {
            return With.TransactionAsync(UnitOfWork, context =>
            {
                return func(context);
            });
        }

        public Task<ServiceResult<T>> ExecuteAsync<T>(Func<IDBContext, Task<ServiceResult<T>>> func)
        {
            return With.TransactionAsync(UnitOfWork, context =>
            {
                return func(context);
            });
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
