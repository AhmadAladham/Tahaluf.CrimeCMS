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
        Task<IEnumerable<Criminal>> Search(CriminalDto criminalDto);
    }
}
