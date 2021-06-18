using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CrimeFile.Core.Common
{
    public interface IDBContext
    {
        void SeedDataBase();
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        IDbTransaction BeginTransaction();

        void Dispose();
    }
}
