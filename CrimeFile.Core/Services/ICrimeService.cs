using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface ICrimeService : IBaseService
    {

        Task<ServiceResult<Crime>> GetById(int id);
        Task<ServiceResult<List<Crime>>> GetAll();
        Task<ServiceResult<int>> Create(Crime crime);
        Task<ServiceResult<int>> Edit(Crime crime);
        Task<ServiceResult<int>> Delete(int id);
        Task<ServiceResult<IEnumerable<Crime>>> Search(CrimeDto crimeDto);
    }
}
