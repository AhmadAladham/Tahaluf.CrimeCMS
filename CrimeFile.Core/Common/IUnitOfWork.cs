using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IDBContext Context { get; }

        /// <summary>
        /// A flag indicates if this Unit of Work will be commited or not at UnitOfWork.End().
        /// </summary>
        bool IsToBeCommitted { get; }

        /// <summary>
        /// This indicates if the Unit of Work should be commited or not.
        /// </summary>
        void SetToBeCommitted();

        /// <summary>
        /// This indicates if the Unit of Work should be rollback or not.
        /// </summary>
        void SetToBeRollback();

        /// <summary>
        /// When overriden it will commit the transaction gracefully IF NEEDED, if not it will not commit NOR rollback, if an exception happened
        /// it will rollback.
        /// </summary>
        void End();
    }
}
