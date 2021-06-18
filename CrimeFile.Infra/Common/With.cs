using CrimeFile.Core.Common;
    using Fluentx;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace CrimeFile.Infra.Common
    {
        internal static class With
        {
            public static TResult Transaction<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, TResult> transactional) where TResult : ServiceResult
            {
                Guard.Against<ArgumentNullException>(transactional.IsNull());

                if (unitOfWork.Context.Transaction == null)
                {
                    unitOfWork.Context.BeginTransaction();
                }
                try
                {
                    TResult result = transactional(unitOfWork.Context);
                    if (result.IsSucceed)
                    {
                        unitOfWork.SetToBeCommitted();
                    }
                    else
                    {
                        unitOfWork.SetToBeRollback();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    unitOfWork.SetToBeRollback();
                    //Logger.Error(ex);
                    throw;
                }
            }

            public static async Task<TResult> TransactionAsync<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, Task<TResult>> transactional) where TResult : ServiceResult
            {
                Guard.Against<ArgumentNullException>(transactional.IsNull());

                if (unitOfWork.Context != null && unitOfWork.Context.Transaction == null)
                {
                    unitOfWork.Context.BeginTransaction();
                }
                try
                {
                    TResult result = await transactional(unitOfWork.Context);
                    if (result.IsSucceed)
                    {
                        unitOfWork.SetToBeCommitted();
                    }
                    else
                    {
                        unitOfWork.SetToBeRollback();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    unitOfWork.SetToBeRollback();
                    // Logger.Error(ex);
                    throw;
                }
            }

            public static void Transaction(IUnitOfWork unitOfWork, Action<IDBContext> transactional)
            {
                Guard.Against<ArgumentNullException>(transactional.IsNull());

                if (unitOfWork.Context.Transaction == null)
                {
                    unitOfWork.Context.BeginTransaction();
                }
                try
                {
                    transactional(unitOfWork.Context);
                    unitOfWork.SetToBeCommitted();
                }
                catch (Exception ex)
                {
                    unitOfWork.SetToBeRollback();
                    // Logger.Error(ex);
                    throw;
                }
            }

            public static async Task TransactionAsync(IUnitOfWork unitOfWork, Func<IDBContext, Task> transactional)
            {
                Guard.Against<ArgumentNullException>(transactional.IsNull());

                if (unitOfWork.Context.Transaction == null)
                {
                    unitOfWork.Context.BeginTransaction();
                }
                try
                {
                    await transactional(unitOfWork.Context);
                    unitOfWork.SetToBeCommitted();
                }
                catch (Exception ex)
                {
                    unitOfWork.SetToBeRollback();
                    // Logger.Error(ex);
                    throw;
                }
            }

            public static TResult Action<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, TResult> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                TResult result = actional(unitOfWork.Context);

                return result;
            }

            public static async Task<TResult> ActionAsync<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, Task<TResult>> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                TResult result = await actional(unitOfWork.Context);

                return result;
            }

            public static void Action(IUnitOfWork unitOfWork, Action<IDBContext> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                actional(unitOfWork.Context);
            }

            public static async Task ActionAsync(IUnitOfWork unitOfWork, Func<IDBContext, Task> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                await actional(unitOfWork.Context);
            }

            public static TResult Func<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, TResult> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                TResult result = actional(unitOfWork.Context);

                return result;
            }

            public static async Task<TResult> FuncAsync<TResult>(IUnitOfWork unitOfWork, Func<IDBContext, Task<TResult>> actional)
            {
                Guard.Against<ArgumentNullException>(actional.IsNull());

                TResult result = await actional(unitOfWork.Context);

                return result;

            }
        }
    }
