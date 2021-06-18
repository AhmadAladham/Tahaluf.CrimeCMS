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
    public class CrimeCategoryRepository : ICrimeCategoryRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;
        public CrimeCategoryRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }

        public async Task<CrimeCategory> GetById(int id)
        {
            queryParameters.Add("@CrimeCategoryId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<CrimeCategory>("GetCrimeCategoryById", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<CrimeCategory>> GetAll()
        {
            var result = await _dbContext.Connection.QueryAsync<CrimeCategory>("GetAllCrimeCategories", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (List<CrimeCategory>)result;
        }

        public async Task<int> Create(CrimeCategory crimeCategory)
        {
            queryParameters.Add("@CrimeCategoryName", crimeCategory.CrimeCategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("CreateCrimeCategory", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Edit(CrimeCategory crimeCategory)
        {
            queryParameters.Add("@CrimeCategoryId", crimeCategory.CrimeCategoryId, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryName", crimeCategory.CrimeCategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("EditCrimeCategory", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            queryParameters.Add("@CrimeCategoryId", id, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteCrimeCategory", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
