using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;
        public UserRoleRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }

        public async Task<List<UserRole>> GetAll()
        {
            var result = await _dbContext.Connection.QueryAsync<UserRole>("GetAllRoles", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (List<UserRole>)result;
        }
    }
}
