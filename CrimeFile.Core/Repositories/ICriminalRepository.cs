using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface ICriminalRepository : IRepository<Criminal>
    {
        Task<Criminal> GetById(int id);
        Task<Criminal> GetByNationalNumber(string nationalNumber);
        Task<PagedList<Criminal>> Search(SearchCriminalsDTO searchCriminalsDTO);
        Task<PagedList<Criminal>> GetAllPaged(CriminalParameters criminalParameters);
    }
}
