using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface IStationRepository : IRepository<Station>
    {
        Task<Station> GetById(int id);
    }
}
