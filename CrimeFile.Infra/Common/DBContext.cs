using CrimeFile.Core.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CrimeFile.Infra.Common
{
    public class DBContext : IDBContext, IDisposable
    {
        public IDbConnection Connection { get; private set; }
        private readonly IConfiguration Configration;
        public IDbTransaction Transaction { get; private set; }
        public DBContext(IConfiguration configuration)
        {
            Configration = configuration;
            CreateConnection();
        }

        public void SeedDataBase()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Transaction != null)
                {
                    Transaction.Dispose();
                    Transaction = null;
                }

                // free managed resources
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                    // Connection = null;
                }
            }
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Transaction == null)
            {
                try
                {
                    Transaction = Connection.BeginTransaction(isolationLevel);
                }
                catch (Exception ex)
                {
                    // Logger.Error(ex, nameof(DbContext));
                }
            }
            return Transaction;
        }

        private void CreateConnection()
        {
            string connectionString = Configration["ConnectionStrings:DBConnectionString"];

            Connection = new SqlConnection(connectionString);
            Connection.Open();
            // BeginTransaction();
        }

        public IDbTransaction BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();

            return Transaction;
        }
    }
}
