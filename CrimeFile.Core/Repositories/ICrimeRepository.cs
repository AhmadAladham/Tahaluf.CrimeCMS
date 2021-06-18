using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface ICrimeRepository : IRepository<Crime>
    {
        Task<IEnumerable<Crime>> Search(CrimeDto crimeDto);
    }
}
