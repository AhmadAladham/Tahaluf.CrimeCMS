using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IStationService : IBaseService
    {
        Task<ServiceResult<Station>> GetById(int id);
        Task<ServiceResult<List<Station>>> GetAll();
        Task<ServiceResult<int>> Create(Station station);
        Task<ServiceResult<int>> Edit(Station station);
        Task<ServiceResult<int>> Delete(int id);
    }
}
