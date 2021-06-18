using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface ICrimeCategoryService: IBaseService
    {
        Task<ServiceResult<CrimeCategory>> GetById(int id);
        Task<ServiceResult<List<CrimeCategory>>> GetAll();
        Task<ServiceResult<int>> Create(CrimeCategory crimeCategory);
        Task<ServiceResult<int>> Edit(CrimeCategory crimeCategory);
        Task<ServiceResult<int>> Delete(int id);
    }
}
