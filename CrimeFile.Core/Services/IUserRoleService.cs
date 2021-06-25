using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IUserRoleService : IBaseService
    {
        Task<ServiceResult<List<UserRole>>> GetAll();
    }
}
