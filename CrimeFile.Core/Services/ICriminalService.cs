using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface ICriminalService : IBaseService
    {
        Task<ServiceResult<List<Criminal>>> GetById(int id);
        Task<ServiceResult<Criminal>> GetByNationalNumber(string nationalNumber);
        Task<ServiceResult<PagedList<Criminal>>> GetAllPaged(CriminalParameters criminalParameters);
        Task<ServiceResult<int>> Create(Criminal criminal);
        Task<ServiceResult<int>> Edit(Criminal criminal);
        Task<ServiceResult<int>> Delete(int id);
        Task<ServiceResult<PagedList<Criminal>>> Search(SearchCriminalsDTO searchCriminalsDTO);
    }
}
