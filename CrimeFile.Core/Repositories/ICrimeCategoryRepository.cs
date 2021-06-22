using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface ICrimeCategoryRepository : IRepository<CrimeCategory>
    {
        Task<CrimeCategory> GetById(int id);
    }
}
