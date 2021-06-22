using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<int> Create(T entity);
        Task<int> Edit(T entity);
        Task<int> Delete(int id);
    }
}
