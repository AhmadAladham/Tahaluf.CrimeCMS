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
        Task<ServiceResult<Criminal>> GetById(int id);
        Task<ServiceResult<List<Criminal>>> GetAll();
        Task<ServiceResult<int>> Create(Criminal criminal);
        Task<ServiceResult<int>> Edit(Criminal criminal);
        Task<ServiceResult<int>> Delete(int id);
        Task<ServiceResult<IEnumerable<Criminal>>> Search(CriminalDto criminalDto);
    }
}
