using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Infra.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDBContext Context { get; private set; }
        public bool IsToBeCommitted { get; private set; }
        public UnitOfWork(IDBContext context)
        {
            Context = context;
        }

        public void SetToBeCommitted()
        {
            IsToBeCommitted = true;
        }

        public void SetToBeRollback()
        {
            IsToBeCommitted = false;
        }
        private void Commit()
        {
            try
            {
                Context.Transaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                IsToBeCommitted = false;
            }
        }

        private void Rollback()
        {
            try
            {
                Context?.Transaction?.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                IsToBeCommitted = false;
            }
        }

        public void End()
        {
            if (IsToBeCommitted)
            {
                try
                {
                    Commit();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    IsToBeCommitted = false;
                }
            }
            else
            {
                Rollback();
            }
        }

        public void Dispose()
        {
            End();

            if (Context != null)
            {
                if (Context.Transaction != null)
                {
                    Context.Transaction.Dispose();
                }
                Context.Dispose();
                //Context = null;
            }
            IsToBeCommitted = false;
        }
    }
}
